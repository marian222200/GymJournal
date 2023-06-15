using GymJournal.App.ViewModel;
using GymJournal.App.ViewModel.UserInfoViewModels;

namespace GymJournal.App.View.UserInfoPages;

public partial class UserRegisterPage : ContentPage
{
	public UserRegisterPage(UserRegisterPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		Title = viewModel.Title;
	}
}