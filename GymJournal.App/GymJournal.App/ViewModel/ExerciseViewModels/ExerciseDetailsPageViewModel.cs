using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View.ExercisePages;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.ExerciseViewModels
{
	[QueryProperty("ExerciseId","ExerciseId")]
	public partial class ExerciseDetailsPageViewModel : BaseViewModel
	{
		private readonly IExerciseService _exerciseService;
		private readonly ExceptionHandlerService _exceptionHandlerService;
		private readonly IdentityService _identityService;

		public ExerciseDetailsPageViewModel(IExerciseService exerciseService, ExceptionHandlerService exceptionHandlerService, IdentityService identityService)
		{
			_exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

			Title = "Exercise Details";
		}

		public Guid ExerciseId { get; set; }

		[ObservableProperty]
		public ExerciseDto detailsExercise;

		[ObservableProperty]
		public bool isAdmin;

		[ObservableProperty]
		public bool isNotAdmin;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				IsAdmin = _identityService.IsAdmin;
				IsNotAdmin = !_identityService.IsAdmin;

				DetailsExercise = await _exerciseService.GetById(ExerciseId);

				Title = DetailsExercise.Name;
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
		public async void Delete()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				await _exerciseService.Delete(DetailsExercise.Id);
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
		public async Task GoToUpdateAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(ExerciseUpsertPage)}", true,
				new Dictionary<string, object>
				{
					{"ExerciseId", ExerciseId}
				});
		}
	}
}
