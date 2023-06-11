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
	public partial class WorkoutPlanListPageViewModel : BaseViewModel
	{
		private readonly IWorkoutPlanService _workoutPlanService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public WorkoutPlanListPageViewModel(IWorkoutPlanService workoutPlanService, ExceptionHandlerService exceptionHandlerService)
		{
			_workoutPlanService = workoutPlanService ?? throw new ArgumentNullException(nameof(workoutPlanService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
		}

		public ObservableCollection<WorkoutPlanDto> WorkoutPlans;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				WorkoutPlans = new ObservableCollection<WorkoutPlanDto>(await _workoutPlanService.GetAll());
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
		public async void Delete(Guid id)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				await _workoutPlanService.Delete(id);
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
