using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Models;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.Services.LocalStorageService;
using GymJournal.App.View;
using GymJournal.App.View.UserInfoPages;
using GymJournal.App.View.WorkoutPages;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
	{
		private readonly IdentityService _identityService;
		private readonly IUserInfoService _userInfoService;
		private readonly ILocalStorageService _localStorageService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public MainPageViewModel(IdentityService identityService, IUserInfoService userInfoService, ILocalStorageService localStorageService, ExceptionHandlerService exceptionHandlerService)
		{
			_identityService = identityService ?? throw new ArgumentException(nameof(identityService));
			_userInfoService = userInfoService ?? throw new ArgumentException(nameof(userInfoService));
			_localStorageService = localStorageService ?? throw new ArgumentException(nameof(localStorageService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentException(nameof(exceptionHandlerService));

			Title = "GymJournal";
		}

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				_identityService.RetrieveIdentity();

				await Shell.Current.GoToAsync($"//{nameof(WorkoutTodayPage)}", true);
			}
			catch (InexistentStoredDataException ex)
			{
			}
			catch (Exception ex)
			{
				await _exceptionHandlerService.HandleException(ex);

				_identityService.DeleteIdentity();
			}
			finally { IsBusy = false; }
		}

		[RelayCommand]
		public async Task GoToLoginAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(UserLoginPage)}", true);
		}

		[RelayCommand]
		public async Task GoToRegisterAsync()
		{
			await Shell.Current.GoToAsync($"{nameof(UserRegisterPage)}", true);
		}
	}
}
