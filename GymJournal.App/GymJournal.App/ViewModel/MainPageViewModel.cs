using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel
{
	public partial class MainPageViewModel : BaseViewModel
	{
		private readonly ExerciseService exerciseService;
		public ObservableCollection<ExerciseDto> Exercises { get; } = new ObservableCollection<ExerciseDto>();
		public MainPageViewModel(ExerciseService exerciseService)
		{
			Title = "Today's Workout";

			this.exerciseService = exerciseService;
		}

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var exercises = await exerciseService.GetAll();

				if (Exercises.Count != 0)
					Exercises.Clear();

				foreach (var exercise in exercises)
					Exercises.Add(exercise);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				await Shell.Current.DisplayAlert("Error!", "An error occurred.", "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}

		[RelayCommand]
		async Task GetAll()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;
				var exercises = await exerciseService.GetAll();

				if (Exercises.Count != 0)
					Exercises.Clear();

				foreach (var exercise in exercises)
					Exercises.Add(exercise);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				await Shell.Current.DisplayAlert("Error!", "An error occurred.", "OK");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
