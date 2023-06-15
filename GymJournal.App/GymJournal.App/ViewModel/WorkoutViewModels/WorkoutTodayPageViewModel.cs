using GymJournal.App.Services;
using GymJournal.App.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.WorkoutViewModels
{
	public partial class WorkoutTodayPageViewModel : BaseViewModel
	{
		private readonly IWorkoutService _workoutService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public WorkoutTodayPageViewModel(IWorkoutService workoutService, ExceptionHandlerService exceptionHandlerService)
		{
			_workoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));

			Title = "Today's Workout";
		}
	}
}