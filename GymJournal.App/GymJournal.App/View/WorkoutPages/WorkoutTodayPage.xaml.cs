using GymJournal.App.ViewModel.WorkoutViewModels;

namespace GymJournal.App.View.WorkoutPages;

public partial class WorkoutTodayPage : ContentPage
{
	public WorkoutTodayPage(WorkoutTodayPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}
}