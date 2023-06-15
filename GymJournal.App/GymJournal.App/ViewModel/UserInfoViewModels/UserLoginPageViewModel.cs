using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View;
using GymJournal.App.View.WorkoutPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel
{
	public partial class UserLoginPageViewModel : BaseViewModel
	{
		private readonly IUserInfoService _userInfoService;
		private readonly ExceptionHandlerService _exceptionHandlerService;
		private readonly IdentityService _identityService;

		public UserLoginPageViewModel(IUserInfoService userInfoService, ExceptionHandlerService exceptionHandlerService, IdentityService identityService)
		{
			_userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

			Title = "Login";
		}

		[ObservableProperty]
		public string inputUserName;

		[ObservableProperty]
		public string inputPassword;

		[RelayCommand]
		public async void Login()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				await _userInfoService.Login(InputUserName, InputPassword);

				_identityService.StoreIdentity();

				await Shell.Current.GoToAsync($"//{nameof(WorkoutTodayPage)}", true);
			}
			catch (Exception ex)
			{
				await _exceptionHandlerService.HandleException(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
