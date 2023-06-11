namespace GymJournal.App.Services
{
	public class IdentityService
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public bool IsAuthenticated { get; set; }
		public string UserRole { get; set; }
	}
}
