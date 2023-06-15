using GymJournal.App.ViewModel;

namespace GymJournal.App.View.UserInfoPages;

public partial class UserLoginPage : ContentPage
{
	public UserLoginPage(UserLoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}
}