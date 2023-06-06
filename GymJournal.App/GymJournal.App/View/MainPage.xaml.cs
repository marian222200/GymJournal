using GymJournal.App.ViewModel;

namespace GymJournal.App.View;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is MainPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}