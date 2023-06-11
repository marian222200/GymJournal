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

namespace GymJournal.App.ViewModel.ExerciseViewModels
{
	public partial class ExerciseListPageViewModel : BaseViewModel
	{
		private readonly IExerciseService _exerciseService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public ExerciseListPageViewModel(IExerciseService exerciseService, ExceptionHandlerService exceptionHandlerService)
		{
			_exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
		}

		public ObservableCollection<ExerciseDto> Exercises;

		public async Task OnAppearing()
		{
			if(IsBusy) return;

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
		public async void Delete(Guid id)
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				await _exerciseService.Delete(id);
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
