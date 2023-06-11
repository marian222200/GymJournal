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
	public partial class UpsertWorkoutPageViewModel : BaseViewModel
	{
		private readonly IWorkoutService _workoutService;
		private readonly IExerciseService _exerciseService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public UpsertWorkoutPageViewModel(IWorkoutService workoutService, IExerciseService exerciseService, ExceptionHandlerService exceptionHandlerService)
		{
			_workoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
			_exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
		}

		[ObservableProperty]
		public WorkoutDto upsertWorkout;

		public ObservableCollection<ExerciseDto> Exercises;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				Exercises = new ObservableCollection<ExerciseDto>(await _exerciseService.GetAll());
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
		public async Task Update()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				UpsertWorkout = await _workoutService.Update(UpsertWorkout);
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
		public async Task Add()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				UpsertWorkout = await _workoutService.Add(UpsertWorkout);
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
