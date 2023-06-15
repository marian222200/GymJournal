using GymJournal.App.ViewModel.UserInfoViewModels;

namespace GymJournal.App.View.UserInfoPages;

public partial class UserListPage : ContentPage
{
	public UserListPage(UserListPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is UserListPageViewModel viewModel)
		{
			await viewModel.OnAppearing();
		}
	}
}