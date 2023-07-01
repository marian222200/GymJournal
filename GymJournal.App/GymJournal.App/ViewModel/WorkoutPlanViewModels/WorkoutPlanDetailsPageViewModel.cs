using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View.WorkoutPages;
using GymJournal.App.View.WorkoutPlanPages;
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
		private readonly IdentityService _identityService;

		public WorkoutPlanDetailsPageViewModel(IWorkoutPlanService workoutPlanService, ExceptionHandlerService exceptionHandlerService, IdentityService identityService)
		{
			_workoutPlanService = workoutPlanService ?? throw new ArgumentNullException(nameof(workoutPlanService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

			Title = "Workout Plan Details";
		}

		public Guid WorkoutPlanId { get; set; }

		[ObservableProperty]
		public WorkoutPlanDto detailsWorkoutPlan;

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

				bool answer = await Shell.Current.DisplayAlert("Are you sure?", $"Are you sure you want to delete {DetailsWorkoutPlan.Name}?", "Yes", "No");

				if (answer)
				{
					await _workoutPlanService.Delete(WorkoutPlanId);
					await Shell.Current.Navigation.PopAsync();
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
		public async Task GoToUpdateAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(WorkoutPlanUpsertPage)}", true,
				new Dictionary<string, object>
				{
					{"WorkoutPlanId", WorkoutPlanId}
				});
		}

		[RelayCommand]
		public async Task ChooseWorkoutPlan()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				bool answer = await Shell.Current.DisplayAlert("Are you sure?", $"Are you sure you want to change to {DetailsWorkoutPlan.Name} plan?", "Yes", "No");

				if (answer)
				{
					await _workoutPlanService.ChooseWorkoutPlan(WorkoutPlanId);
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
	}
}
