using GymJournal.App.View.ExercisePages;
using GymJournal.App.View.UserInfoPages;
using GymJournal.App.View.WorkoutPages;
using GymJournal.App.View.WorkoutPlanPages;

namespace GymJournal.App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		//Routing.RegisterRoute(nameof(UserEditPage), typeof(UserEditPage));
		//Routing.RegisterRoute(nameof(UserListPage), typeof(UserListPage));
		Routing.RegisterRoute(nameof(UserLoginPage), typeof(UserLoginPage));
		//Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));
		Routing.RegisterRoute(nameof(UserRegisterPage), typeof(UserRegisterPage));

		Routing.RegisterRoute(nameof(ExerciseDetailsPage), typeof(ExerciseDetailsPage));
		//Routing.RegisterRoute(nameof(ExerciseListPage), typeof(ExerciseListPage));
		//Routing.RegisterRoute(nameof(ExerciseUpsertPage), typeof(ExerciseUpsertPage));

		Routing.RegisterRoute(nameof(WorkoutDetailsPage), typeof(WorkoutDetailsPage));
		//Routing.RegisterRoute(nameof(WorkoutListPage), typeof(WorkoutListPage));
		//Routing.RegisterRoute(nameof(WorkoutTodayPage), typeof(WorkoutTodayPage));
		Routing.RegisterRoute(nameof(WorkoutUpsertPage), typeof(WorkoutUpsertPage));

		Routing.RegisterRoute(nameof(WorkoutPlanDetailsPage), typeof(WorkoutPlanDetailsPage));
		//Routing.RegisterRoute(nameof(WorkoutPlanListPage), typeof(WorkoutPlanListPage));
		Routing.RegisterRoute(nameof(WorkoutPlanUpsertPage), typeof(WorkoutPlanUpsertPage));
	}
}
