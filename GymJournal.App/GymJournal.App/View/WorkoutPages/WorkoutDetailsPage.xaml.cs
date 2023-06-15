using GymJournal.App.ViewModel.WorkoutViewModels;

namespace GymJournal.App.View.WorkoutPages;

public partial class WorkoutDetailsPage : ContentPage
{
	public WorkoutDetailsPage(WorkoutDetailsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkoutDetailsPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}