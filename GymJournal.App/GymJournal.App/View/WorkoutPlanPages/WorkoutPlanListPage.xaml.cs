using GymJournal.App.ViewModel.WorkoutPlanViewModels;

namespace GymJournal.App.View.WorkoutPlanPages;

public partial class WorkoutPlanListPage : ContentPage
{
	public WorkoutPlanListPage(WorkoutPlanListPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkoutPlanListPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}