using GymJournal.App.ViewModel.WorkoutViewModels;

namespace GymJournal.App.View.WorkoutPages;

public partial class WorkoutListPage : ContentPage
{
	public WorkoutListPage(WorkoutListPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkoutListPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}