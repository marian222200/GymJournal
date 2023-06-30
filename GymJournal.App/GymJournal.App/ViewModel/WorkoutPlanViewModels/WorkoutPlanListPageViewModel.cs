using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View.WorkoutPlanPages;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.WorkoutPlanViewModels
{
	public partial class WorkoutPlanListPageViewModel : BaseViewModel
	{
		private readonly IWorkoutPlanService _workoutPlanService;
		private readonly ExceptionHandlerService _exceptionHandlerService;
		private readonly IdentityService _identityService;

		public WorkoutPlanListPageViewModel(IWorkoutPlanService workoutPlanService, ExceptionHandlerService exceptionHandlerService, IdentityService identityService)
		{
			_workoutPlanService = workoutPlanService ?? throw new ArgumentNullException(nameof(workoutPlanService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

			Title = "Explore";
		}

		public ObservableCollection<WorkoutPlanDto> WorkoutPlans { get; } = new();

		[ObservableProperty]
		public bool isAdmin;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				IsAdmin = _identityService.IsAdmin;

				var workoutPlans = new ObservableCollection<WorkoutPlanDto>(await _workoutPlanService.GetAll());

				if (WorkoutPlans.Count > 0)
					WorkoutPlans.Clear();

				foreach (var workoutPlan in workoutPlans)
					WorkoutPlans.Add(workoutPlan);
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
		public async Task GoToDetailsAsync(WorkoutPlanDto workoutPlan)
		{
			if (workoutPlan is null)
				return;

			await Shell.Current.GoToAsync($"{nameof(WorkoutPlanDetailsPage)}", true,
				new Dictionary<string, object>
				{
					{"WorkoutPlanId", workoutPlan.Id}
				});
		}

		[RelayCommand]
		public async Task GoToAddAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(WorkoutPlanUpsertPage)}", true,
				new Dictionary<string, object>
				{
					{"WorkoutPlanId", Guid.Empty}
				});
		}
	}
}
