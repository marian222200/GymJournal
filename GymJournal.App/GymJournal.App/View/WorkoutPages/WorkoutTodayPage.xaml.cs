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

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkoutTodayPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}