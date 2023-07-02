using CommunityToolkit.Mvvm.ComponentModel;
using GymJournal.App.Services;
using GymJournal.App.View.ExercisePages;
using GymJournal.App.View.UserInfoPages;
using GymJournal.App.View.WorkoutPages;
using GymJournal.App.View.WorkoutPlanPages;
using GymJournal.App.View.WorkSetPages;
using GymJournal.App.ViewModel;

namespace GymJournal.App;

public partial class AppShell : Shell
{
	public readonly IdentityService _identityService;

	public AppShell(IdentityService identityService)
	{
		InitializeComponent();

		_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
		BindingContext = new AppShellViewModel(_identityService);

		Routing.RegisterRoute(nameof(UserLoginPage), typeof(UserLoginPage));
		Routing.RegisterRoute(nameof(UserRegisterPage), typeof(UserRegisterPage));

		Routing.RegisterRoute(nameof(ExerciseDetailsPage), typeof(ExerciseDetailsPage));
		Routing.RegisterRoute(nameof(ExerciseUpsertPage), typeof(ExerciseUpsertPage));

		Routing.RegisterRoute(nameof(WorkoutDetailsPage), typeof(WorkoutDetailsPage));
		Routing.RegisterRoute(nameof(WorkoutUpsertPage), typeof(WorkoutUpsertPage));

		Routing.RegisterRoute(nameof(WorkoutPlanDetailsPage), typeof(WorkoutPlanDetailsPage));
		Routing.RegisterRoute(nameof(WorkoutPlanUpsertPage), typeof(WorkoutPlanUpsertPage));

		Routing.RegisterRoute(nameof(WorkSetUpsertPage), typeof(WorkSetUpsertPage));
		Routing.RegisterRoute(nameof(WorkSetListPage), typeof(WorkSetListPage));
	}
}
