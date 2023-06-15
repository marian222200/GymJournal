using GymJournal.App.ViewModel.WorkoutViewModels;

namespace GymJournal.App.View.WorkoutPages;

public partial class WorkoutUpsertPage : ContentPage
{
	public WorkoutUpsertPage(WorkoutUpsertPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkoutUpsertPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}