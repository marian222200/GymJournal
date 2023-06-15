using GymJournal.App.ViewModel.ExerciseViewModels;

namespace GymJournal.App.View.ExercisePages;

public partial class ExerciseListPage : ContentPage
{
	public ExerciseListPage(ExerciseListPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is ExerciseListPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}