using GymJournal.App.ViewModel.ExerciseViewModels;

namespace GymJournal.App.View.ExercisePages;

public partial class ExerciseDetailsPage : ContentPage
{
	public ExerciseDetailsPage(ExerciseDetailsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is ExerciseDetailsPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}