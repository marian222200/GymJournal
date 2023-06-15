using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.Services.InputValidatorService;
using GymJournal.App.View.WorkoutPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel
{
	public partial class UserRegisterPageViewModel : BaseViewModel
	{
		private readonly IUserInfoService _userInfoService;
		private readonly ExceptionHandlerService _exceptionHandlerService;
		private readonly IInputValidatorService _inputValidatorService;
		private readonly IdentityService _identityService;

		public UserRegisterPageViewModel(IUserInfoService userInfoService, ExceptionHandlerService exceptionHandlerService, IInputValidatorService inputValidatorService,
			IdentityService identityService)
		{
			_userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
			_exceptionHandlerService = exceptionHandlerService ?? throw new ArgumentNullException(nameof(exceptionHandlerService));
			_inputValidatorService = inputValidatorService ?? throw new ArgumentNullException(nameof(inputValidatorService));
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
		}

		[ObservableProperty]
		public string inputUserName;

		[ObservableProperty]
		public string inputPassword;

		[ObservableProperty]
		public string inputRepeatPassword;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsUserNameInvalid))]
		public string userNameValidationResult;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsPasswordInvalid))]
		public string passwordValidationResult;

		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(IsRepeatPasswordInvalid))]
		public string repeatPasswordValidationResult;

		public bool IsUserNameInvalid => UserNameValidationResult != null;
		public bool IsPasswordInvalid => PasswordValidationResult != null;
		public bool IsRepeatPasswordInvalid => RepeatPasswordValidationResult != null;


		[RelayCommand]
		public async void Register()
		{
			if (IsBusy) return;

			try
			{
				IsBusy = true;

				UserNameValidationResult = _inputValidatorService.ValidateUserName(InputUserName);
				PasswordValidationResult = _inputValidatorService.ValidatePassword(InputPassword);
				RepeatPasswordValidationResult = _inputValidatorService.ValidateRepeatPassword(InputPassword, InputRepeatPassword);

				if (!IsUserNameInvalid && !IsPasswordInvalid && !IsRepeatPasswordInvalid)
				{
					await _userInfoService.Add(InputUserName, InputPassword);

					_identityService.StoreIdentity();

					await Shell.Current.GoToAsync($"//{nameof(WorkoutTodayPage)}", true);
				}
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
