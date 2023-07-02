using GymJournal.App.ViewModel.WorkSetViewModels;

namespace GymJournal.App.View.WorkSetPages;

public partial class WorkSetListPage : ContentPage
{
	public WorkSetListPage(WorkSetListPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is WorkSetListPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}