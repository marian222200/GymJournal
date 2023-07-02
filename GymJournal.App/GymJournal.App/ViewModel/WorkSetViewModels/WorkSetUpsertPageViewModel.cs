using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.WorkSetViewModels
{
	[QueryProperty("ExerciseId", "ExerciseId")]
	public partial class WorkSetUpsertPageViewModel : BaseViewModel
    {
		private readonly IWorkSetService _workSetService;
		private readonly IdentityService _identityService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public WorkSetUpsertPageViewModel(IWorkSetService workSetService, ExceptionHandlerService exceptionHandlerService, IdentityService identityService)
		{
			_workSetService = workSetService ?? throw new ArgumentNullException(nameof(workSetService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

			Title = "Add Work Set";
		}

		public Guid ExerciseId { get; set; }

		[ObservableProperty]
		public WorkSetDto upsertWorkSet;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				UpsertWorkSet = new WorkSetDto();
			}
			catch (Exception ex)
			{
				await _exceptionHandlerService.HandleException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		[RelayCommand]
		public async Task SendForm()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				var workSet = await _workSetService.Add(new WorkSetDto
				{
					Date = DateTime.Now.ToString(),
					Weight = UpsertWorkSet.Weight,
					Reps = UpsertWorkSet.Reps,
					ExerciseId = ExerciseId,
					UserId = _identityService.UserId,
				});

				await Shell.Current.Navigation.PopAsync();
			}
			catch (Exception ex)
			{
				await _exceptionHandlerService.HandleException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
