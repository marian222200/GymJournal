using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.Domain.DTOs;
using IntelliJ.Lang.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.ExerciseViewModels
{
	public partial class ExerciseUpsertPageViewModel : BaseViewModel
	{
		private readonly IExerciseService _exerciseService;
		private readonly IMuscleService _muscleService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public ExerciseUpsertPageViewModel(IExerciseService exerciseService, IMuscleService muscleService, ExceptionHandlerService exceptionHandlerService)
		{
			_exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
			_muscleService = muscleService ?? throw new ArgumentNullException(nameof(muscleService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
		}

		[ObservableProperty]
		public ExerciseDto upsertExercise;

		public ObservableCollection<MuscleDto> Muscles;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				Muscles = new ObservableCollection<MuscleDto>(await _muscleService.GetAll());
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

				UpsertExercise = await _exerciseService.Update(UpsertExercise);
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

				UpsertExercise = await _exerciseService.Add(UpsertExercise);
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
