using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Models;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View.WorkSetPages;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.WorkoutViewModels
{
	public partial class WorkoutTodayPageViewModel : BaseViewModel
	{
		private readonly IExerciseService _exerciseService;
		private readonly IWorkoutService _workoutService;
		private readonly IWorkoutPlanService _workoutPlanService;
		private readonly ExceptionHandlerService _exceptionHandlerService;
		private readonly IdentityService _identityService;
		private readonly IUserInfoService _userInfoService;
		private readonly IWorkSetService _workSetService;

		public WorkoutTodayPageViewModel(IExerciseService exerciseService, IWorkoutService workoutService, 
			ExceptionHandlerService exceptionHandlerService, IdentityService identityService, IUserInfoService userInfoService,
			IWorkoutPlanService workoutPlanService, IWorkSetService workSetService)
		{
			_exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
			_workoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
			_workoutPlanService = workoutPlanService ?? throw new ArgumentNullException(nameof(workoutPlanService));
			_workSetService = workSetService ?? throw new ArgumentNullException(nameof(workSetService));

			Title = "Today's Workout";
		}

		private UserInfoDto CurrentUser { get; set; }

		[ObservableProperty]
		public string todaysWorkoutName;
		public ObservableCollection<WorkoutTodayViewModel> Exercises { get; } = new();

		[ObservableProperty]
		public bool isEmpty;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				CurrentUser = await _userInfoService.GetById(_identityService.UserId);

				if (CurrentUser.WorkoutPlanId == Guid.Empty)
				{
					Exercises.Clear();
					TodaysWorkoutName = "";

					IsEmpty = true; return;
				}

				IsEmpty = false;

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
		public async Task GoToAddWorkSet(ExerciseDto exercise)
		{
			if (exercise is null)
				return;

			await Shell.Current.GoToAsync($"{nameof(WorkSetUpsertPage)}", true,
				new Dictionary<string, object>
				{
					{"ExerciseId", exercise.Id}
				});
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

		private async Task LoadData()
		{

			var workoutPlan = await _workoutPlanService.GetById(CurrentUser.WorkoutPlanId);

			var timeSinceStarted = DateTime.Now.Date.Subtract(DateTime.Parse(CurrentUser.WorkoutPlanStart).Date).Days;

			var todaysWorkout = await _workoutService.GetById(workoutPlan.Workouts.OrderBy(w => w.Name).ToList()[timeSinceStarted % workoutPlan.Workouts.Count].Id);

			var todaysWorkSets = (await _workSetService.GetAll()).Where(
				w => w.UserId == CurrentUser.Id && DateTime.Parse(w.Date).Date == DateTime.Now.Date);

			TodaysWorkoutName = todaysWorkout.Name;

			Exercises.Clear();

			foreach (var exercise in todaysWorkout.Exercises)
			{
				Exercises.Add(new WorkoutTodayViewModel
				{
					Exercise = exercise,
					WorkSets = todaysWorkSets.Where(w => w.ExerciseId == exercise.Id).ToList(),
				});
			}
		}
	}
}