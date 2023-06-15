using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View.WorkoutPages;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.WorkoutViewModels
{
	public partial class WorkoutListPageViewModel : BaseViewModel
	{
		private readonly IWorkoutService _workoutService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public WorkoutListPageViewModel(IWorkoutService workoutService, ExceptionHandlerService exceptionHandlerService)
		{
			_workoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));

			Title = "Explore";
		}

		public ObservableCollection<WorkoutDto> Workouts { get; } = new();

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				var workouts = new ObservableCollection<WorkoutDto>(await _workoutService.GetAll());

				if (Workouts.Count > 0)
					Workouts.Clear();

				foreach (var workout in workouts)
					Workouts.Add(workout);
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
		public async Task GoToDetailsAsync(WorkoutDto workout)
		{
			if (workout is null)
				return;

			await Shell.Current.GoToAsync($"{nameof(WorkoutDetailsPage)}", true,
				new Dictionary<string, object>
				{
					{"WorkoutId", workout.Id}
				});
		}

		[RelayCommand]
		public async Task GoToAddAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(WorkoutUpsertPage)}", true,
				new Dictionary<string, object>
				{
					{"WorkoutId", Guid.Empty}
				});
		}
	}
}
