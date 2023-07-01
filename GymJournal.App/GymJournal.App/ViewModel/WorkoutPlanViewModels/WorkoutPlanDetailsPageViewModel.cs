using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View.WorkoutPages;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.WorkoutPlanViewModels
{
	[QueryProperty("WorkoutPlanId", "WorkoutPlanId")]
	public partial class WorkoutPlanDetailsPageViewModel : BaseViewModel
	{
		private readonly IWorkoutPlanService _workoutPlanService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public WorkoutPlanDetailsPageViewModel(IWorkoutPlanService workoutPlanService, ExceptionHandlerService exceptionHandlerService)
		{
			_workoutPlanService = workoutPlanService ?? throw new ArgumentNullException(nameof(workoutPlanService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));

			Title = "Workout Plan Details";
		}

		public Guid WorkoutPlanId { get; set; }

		[ObservableProperty]
		public WorkoutPlanDto detailsWorkoutPlan;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				DetailsWorkoutPlan = await _workoutPlanService.GetById(WorkoutPlanId);

				Title = DetailsWorkoutPlan.Name;
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

				await _workoutPlanService.Delete(DetailsWorkoutPlan.Id);
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
	}
}
