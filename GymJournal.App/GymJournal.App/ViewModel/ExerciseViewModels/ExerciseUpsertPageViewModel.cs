using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Models;
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
	[QueryProperty("ExerciseId", "ExerciseId")]
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

			SelectedMuscles = new ObservableCollection<object>();
		}

		public Guid ExerciseId { get; set; }
		[ObservableProperty]
		public ExerciseDto upsertExercise;
		[ObservableProperty]
		public string buttonName;

		public ObservableCollection<MuscleDto> Muscles { get; } = new();
		ObservableCollection<object> selectedMuscles;
		public ObservableCollection<object> SelectedMuscles
		{
			get
			{
				return selectedMuscles;
			}
			set
			{
				if (selectedMuscles != value)
				{
					selectedMuscles = value;
				}
			}
		}

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				if (ExerciseId == Guid.Empty)
				{
					Title = "Add a new Exercise";
					ButtonName = "Add";
					UpsertExercise = new ExerciseDto();
				}
				else
				{
					Title = "Update Exercise";
					ButtonName = "Update";
				}

				var muscles = new ObservableCollection<MuscleDto>(await _muscleService.GetAll());

				if (Muscles.Count > 0)
					Muscles.Clear();

				foreach (var muscle in muscles)
					Muscles.Add(muscle);

				if (ExerciseId != Guid.Empty)
				{
					UpsertExercise = await _exerciseService.GetById(ExerciseId);
					foreach (var muscle in Muscles)
					{
						if (UpsertExercise.Muscles.Any(m => m.Id == muscle.Id))
						{
							SelectedMuscles.Add(muscle);
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

				var muscles = SelectedMuscles.Cast<MuscleDto>().Select(m => new MuscleDto
				{
					Id = m.Id,
					Name = m.Name,
					Exercises = m.Exercises,
				});

				UpsertExercise.Muscles = new List<MuscleDto>(muscles);

				if (ExerciseId == Guid.Empty)
					UpsertExercise = await _exerciseService.Add(UpsertExercise);
				else
					UpsertExercise = await _exerciseService.Update(UpsertExercise);

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
