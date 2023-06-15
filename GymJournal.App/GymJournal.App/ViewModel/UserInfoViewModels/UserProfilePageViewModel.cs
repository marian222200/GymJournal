using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel.UserInfoViewModels
{
	public partial class UserProfilePageViewModel : BaseViewModel
	{
		private readonly IUserInfoService _userInfoService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public UserProfilePageViewModel(IUserInfoService userInfoService, ExceptionHandlerService exceptionHandlerService)
		{
			_userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
		}

		[ObservableProperty]
		public UserInfoDto profileUser;

		public async Task OnAppearing()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				ProfileUser = await _userInfoService.GetById(ProfileUser.Id);
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

		[RelayCommand]
		public async void Delete()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				await _userInfoService.Delete(ProfileUser.Id);
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
