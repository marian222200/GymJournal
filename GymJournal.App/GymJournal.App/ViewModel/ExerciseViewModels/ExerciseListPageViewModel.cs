using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View.ExercisePages;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.ExerciseViewModels
{
	public partial class ExerciseListPageViewModel : BaseViewModel
	{
		private readonly IExerciseService _exerciseService;
		private readonly ExceptionHandlerService _exceptionHandlerService;
		private readonly IdentityService _identityService;

		public ExerciseListPageViewModel(IExerciseService exerciseService, ExceptionHandlerService exceptionHandlerService, IdentityService identityService)
		{
			_exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

			Title = "Explore";
		}


		public ObservableCollection<ExerciseDto> Exercises { get; } = new();

		[ObservableProperty]
		public bool isAdmin;

		public async Task OnAppearing()
		{
			if(IsBusy) return;

			try
			{
				IsBusy = true;

				IsAdmin = _identityService.IsAdmin;

				var exercises = new ObservableCollection<ExerciseDto>(await _exerciseService.GetAll());

				if (Exercises.Count > 0)
					Exercises.Clear();

				foreach (var exercise in exercises)
					Exercises.Add(exercise);
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
		public async Task GoToDetailsAsync(ExerciseDto exercise)
		{
			if (exercise is null)
				return;

			await Shell.Current.GoToAsync($"{nameof(ExerciseDetailsPage)}", true,
				new Dictionary<string, object>
				{
					{"ExerciseId", exercise.Id}
				});
		}

		[RelayCommand]
		public async Task GoToAddAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(ExerciseUpsertPage)}", true,
				new Dictionary<string, object>
				{
					{"ExerciseId", Guid.Empty}
				});
		}
	}
}
