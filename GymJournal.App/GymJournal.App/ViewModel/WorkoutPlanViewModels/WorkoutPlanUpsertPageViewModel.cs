using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.WorkoutPlanViewModels
{
	[QueryProperty("WorkoutPlanId","WorkoutPlanId")]
	public partial class WorkoutPlanUpsertPageViewModel : BaseViewModel
	{
		private readonly IWorkoutPlanService _workoutPlanService;
		private readonly IWorkoutService _workoutService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public WorkoutPlanUpsertPageViewModel(IWorkoutPlanService workoutPlanService, IWorkoutService workoutService, ExceptionHandlerService exceptionHandlerService)
		{
			_workoutPlanService = workoutPlanService ?? throw new ArgumentNullException(nameof(workoutPlanService));
			_workoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));

			SelectedWorkouts = new ObservableCollection<object>();
		}

		public Guid WorkoutPlanId { get; set; }
		[ObservableProperty]
		public WorkoutPlanDto upsertWorkoutPlan;
		[ObservableProperty]
		public string buttonName;

		public ObservableCollection<WorkoutDto> Workouts { get; } = new();
		ObservableCollection<object> selectedWorkouts;
		public ObservableCollection<object> SelectedWorkouts
		{
			get
			{
				return selectedWorkouts;
			}
			set
			{
				if(selectedWorkouts != value)
				{
					selectedWorkouts = value;
				}
			}
		}

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				if (WorkoutPlanId == Guid.Empty)
				{
					Title = "Add a new WorkoutPlan";
					ButtonName = "Add";
					UpsertWorkoutPlan = new WorkoutPlanDto();
				}
				else
				{
					Title = "Update WorkoutPlan";
					ButtonName = "Update";
				}

				var workouts = new ObservableCollection<WorkoutDto>(await _workoutService.GetAll());

				if (workouts.Count > 0)
					Workouts.Clear();
				
				foreach (var workout in workouts)
					Workouts.Add(workout);

				if (WorkoutPlanId != Guid.Empty)
				{
					UpsertWorkoutPlan = await _workoutPlanService.GetById(WorkoutPlanId);
					foreach (var workout in Workouts)
					{
						if(UpsertWorkoutPlan.Workouts.Any(w => w.Id == workout.Id))
						{
							SelectedWorkouts.Add(workout);
						}
					}
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

		[RelayCommand]
		public async Task SendForm()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				var workouts = SelectedWorkouts.Cast<WorkoutDto>().Select(w => new WorkoutDto
				{
					Id = w.Id,
					Name = w.Name,
					Exercises = w.Exercises,
				});

				UpsertWorkoutPlan.Workouts = new List<WorkoutDto>(workouts);

				if (WorkoutPlanId == Guid.Empty)
					UpsertWorkoutPlan = await _workoutPlanService.Add(UpsertWorkoutPlan);
				else
					UpsertWorkoutPlan = await _workoutPlanService.Update(UpsertWorkoutPlan);

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
