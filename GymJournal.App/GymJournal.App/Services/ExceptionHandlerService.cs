using GymJournal.App.Models;
using GymJournal.App.View;

namespace GymJournal.App.Services
{
	public class ExceptionHandlerService
	{
		private readonly IdentityService _identityService;
		public ExceptionHandlerService(IdentityService identityService)
		{
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
		}
		public async Task HandleException(Exception ex)
		{

			if (ex is ServerRequestException)
			{
				if (ex.Message.Contains("invalid UserToken"))
				{
					_identityService.DeleteIdentity();
					await Shell.Current.DisplayAlert("Signed Out", "You have been signed out.", "OK");
					await Shell.Current.GoToAsync($"//{nameof(MainPage)}", true);
				}
				else
					await Shell.Current.DisplayAlert("Server Request Error", ex.Message, "OK");
			}
			else
			{
				await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
			}
		}
	}
}
