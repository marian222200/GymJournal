using GymJournal.App.ViewModel.WorkoutPlanViewModels;

namespace GymJournal.App.View.WorkoutPlanPages;

public partial class WorkoutPlanUpsertPage : ContentPage
{
	public WorkoutPlanUpsertPage(WorkoutPlanUpsertPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkoutPlanUpsertPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}