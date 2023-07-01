using GymJournal.App.ViewModel.WorkSetViewModels;

namespace GymJournal.App.View.WorkSetPages;

public partial class WorkSetUpsertPage : ContentPage
{
	public WorkSetUpsertPage(WorkSetUpsertPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkSetUpsertPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}