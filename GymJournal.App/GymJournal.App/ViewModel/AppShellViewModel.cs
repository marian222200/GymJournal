using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymJournal.App.Services;
using GymJournal.App.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.ViewModel
{
	public partial class AppShellViewModel : BaseViewModel
	{
		private readonly IdentityService _identityService;

		public AppShellViewModel(IdentityService identityService)
		{
			_identityService = identityService ?? throw new ArgumentException(nameof(identityService));
		}

		[RelayCommand]
		public async Task SignOutAsync()
		{
			_identityService.DeleteIdentity();

			await Shell.Current.GoToAsync($"//{nameof(MainPage)}", true);
		}
	}
}
