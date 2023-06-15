using GymJournal.App.Models;

namespace GymJournal.App.Services
{
	public class IdentityService
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public bool IsAuthenticated { get; set; }
		public string UserRole { get; set; }

		public void StoreIdentity()
		{
			Preferences.Default.Set(nameof(UserId), UserId.ToString());
			Preferences.Default.Set(nameof(UserToken), UserToken.ToString());
			Preferences.Default.Set(nameof(IsAuthenticated), IsAuthenticated);
			Preferences.Default.Set(nameof(UserRole), UserRole);
		}

		public void RetrieveIdentity()
		{
			if (Preferences.Default.ContainsKey(nameof(UserId)) 
				&& Preferences.Default.ContainsKey(nameof(UserToken)) 
				&& Preferences.Default.ContainsKey(nameof(IsAuthenticated))
				&& Preferences.Default.ContainsKey(nameof(UserRole)))
			{
				UserId = Guid.Parse(Preferences.Default.Get(nameof(UserId), ""));
				UserToken = Guid.Parse(Preferences.Default.Get(nameof(UserToken), ""));
				IsAuthenticated = Preferences.Default.Get(nameof (IsAuthenticated), false);
				UserRole = Preferences.Default.Get(nameof(UserRole), "");
			}
			else
			{
				throw new InexistentStoredDataException("No previous login");
			}
		}

		public void DeleteIdentity()
		{
			Preferences.Default.Remove(nameof(UserId));
			Preferences.Default.Remove(nameof(UserToken));
			Preferences.Default.Remove(nameof(IsAuthenticated));
			Preferences.Default.Remove(nameof(UserRole));
		}
	}
}
