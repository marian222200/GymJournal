using GymJournal.App.ViewModel.ExerciseViewModels;

namespace GymJournal.App.View.ExercisePages;

public partial class ExerciseUpsertPage : ContentPage
{
	public ExerciseUpsertPage(ExerciseUpsertPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is ExerciseUpsertPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}