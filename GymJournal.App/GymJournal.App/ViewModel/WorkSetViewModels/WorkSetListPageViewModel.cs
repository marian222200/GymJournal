using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Models;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.WorkSetViewModels
{
	[QueryProperty("ExerciseId", "ExerciseId")]
	public partial class WorkSetListPageViewModel : BaseViewModel
	{
		private readonly IWorkSetService _workSetService;
		private readonly IdentityService _identityService;
		private readonly ExceptionHandlerService _exceptionHandlerService;
		private readonly IExerciseService _exerciseService;

		public WorkSetListPageViewModel(IWorkSetService workSetService, ExceptionHandlerService exceptionHandlerService, IdentityService identityService, IExerciseService exerciseService)
		{
			_workSetService = workSetService ?? throw new ArgumentNullException(nameof(workSetService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));

			Title = "History";
		}

		public Guid ExerciseId { get; set; }

		[ObservableProperty]
		public string exerciseName;

		public ObservableCollection<WorkSetListViewModel> WorkSets { get; } = new ();

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				ExerciseName = (await _exerciseService.GetById(ExerciseId)).Name;

				await LoadData();
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
		public async void Delete(Guid workSetId)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				bool answer = await Shell.Current.DisplayAlert("Are you sure?", $"Are you sure you want to delete the set?", "Yes", "No");

				if (answer)
				{
					await _workSetService.Delete(workSetId);

					await LoadData();
				}
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

		public async Task LoadData()
		{
			var workSets = (await _workSetService.GetAll()).Where(w => w.ExerciseId == ExerciseId && w.UserId == _identityService.UserId);
			var dates = workSets.Select(w => DateTime.Parse(w.Date).Date).Distinct();

			if (WorkSets.Count > 0)
				WorkSets.Clear();

			foreach (var date in dates)
			{
				WorkSets.Add(new WorkSetListViewModel
				{
					Date = date.ToString("dd.MM.yyy"),
					WorkSets = workSets.Where(w => date.Equals(DateTime.Parse(w.Date).Date)).ToList(),
				});
			}
		}
	}
}
