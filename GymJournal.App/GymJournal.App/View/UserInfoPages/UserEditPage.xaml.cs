using GymJournal.App.ViewModel.UserInfoViewModels;

namespace GymJournal.App.View.UserInfoPages;

public partial class UserEditPage : ContentPage
{
	public UserEditPage(UserEditPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}
}