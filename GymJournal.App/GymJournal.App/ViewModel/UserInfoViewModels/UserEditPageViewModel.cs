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
	public partial class UserEditPageViewModel : BaseViewModel
	{
		private readonly IUserInfoService _userInfoService;
		private readonly ExceptionHandlerService _exceptionHandlerService;

		public UserEditPageViewModel(IUserInfoService userInfoService, ExceptionHandlerService exceptionHandlerService)
		{
			_userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
		}

		[ObservableProperty]
		public UserInfoDto editUser;

		[ObservableProperty]
		public string editPassword;

		[RelayCommand]
		public async void Update()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				await _userInfoService.Update(EditUser, EditPassword);
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
