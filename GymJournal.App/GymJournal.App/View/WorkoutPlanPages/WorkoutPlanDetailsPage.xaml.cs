using GymJournal.App.ViewModel.WorkoutPlanViewModels;

namespace GymJournal.App.View.WorkoutPlanPages;

public partial class WorkoutPlanDetailsPage : ContentPage
{
	public WorkoutPlanDetailsPage(WorkoutPlanDetailsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkoutPlanDetailsPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}