using GymJournal.App.ViewModel.UserInfoViewModels;

namespace GymJournal.App.View.UserInfoPages;

public partial class UserProfilePage : ContentPage
{
	public UserProfilePage(UserProfilePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is UserProfilePageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}