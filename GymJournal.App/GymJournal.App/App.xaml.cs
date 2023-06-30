using GymJournal.App.Services;

namespace GymJournal.App;

public partial class App : Application
{
	private IdentityService _identityService;
	public App(IdentityService identityService)
	{
		_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

		InitializeComponent();

		MainPage = new AppShell(_identityService);
	}
}
