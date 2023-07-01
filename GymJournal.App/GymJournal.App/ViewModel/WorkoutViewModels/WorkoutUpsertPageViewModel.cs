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

namespace GymJournal.App.ViewModel.WorkoutViewModels
{
	[QueryProperty("WorkoutId", "WorkoutId")]
	public partial class WorkoutUpsertPageViewModel : BaseViewModel
	{
		private readonly IWorkoutService _workoutService;
		private readonly IExerciseService _exerciseService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public WorkoutUpsertPageViewModel(IWorkoutService workoutService, IExerciseService exerciseService, ExceptionHandlerService exceptionHandlerService)
		{
			_workoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
			_exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));

			SelectedExercises = new ObservableCollection<object>();
		}

		public Guid WorkoutId { get; set; }
		[ObservableProperty]
		public WorkoutDto upsertWorkout;
		[ObservableProperty]
		public string buttonName;

		public ObservableCollection<ExerciseDto> Exercises { get; } = new();
		ObservableCollection<object> selectedExercises;
		public ObservableCollection<object> SelectedExercises
		{
			get
			{
				return selectedExercises;
			}
			set
			{
				if (selectedExercises != value)
				{
					selectedExercises = value;
				}
			}
		}
		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				if (WorkoutId == Guid.Empty)
				{
					Title = "Add a new Workout";
					ButtonName = "Add";
					UpsertWorkout = new WorkoutDto();
				}
				else
				{
					Title = "Update Workout";
					ButtonName = "Update";
				}

				var exercises = new ObservableCollection<ExerciseDto>(await _exerciseService.GetAll());
				
				if (Exercises.Count > 0)
					Exercises.Clear();

				foreach (var exercise in exercises)
					Exercises.Add(exercise);

				if(WorkoutId != Guid.Empty)
				{
					UpsertWorkout = await _workoutService.GetById(WorkoutId);
					foreach (var exercise in Exercises)
					{
						if(UpsertWorkout.Exercises.Any(e => e.Id == exercise.Id))
						{
							SelectedExercises.Add(exercise);
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

				var exercises = SelectedExercises.Cast<ExerciseDto>().Select(e => new ExerciseDto
				{
					Id = e.Id,
					Name = e.Name,
					Workouts = e.Workouts,
				});

				UpsertWorkout.Exercises = new List<ExerciseDto>(exercises);

				if (WorkoutId == Guid.Empty)
					UpsertWorkout = await _workoutService.Add(UpsertWorkout);
				else
					UpsertWorkout = await _workoutService.Update(UpsertWorkout);

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
