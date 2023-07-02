using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.Services.InputValidatorService;
using GymJournal.App.Services.LocalStorageService;
using GymJournal.App.View;
using GymJournal.App.View.ExercisePages;
using GymJournal.App.View.UserInfoPages;
using GymJournal.App.View.WorkoutPages;
using GymJournal.App.View.WorkoutPlanPages;
using GymJournal.App.View.WorkSetPages;
using GymJournal.App.ViewModel;
using GymJournal.App.ViewModel.ExerciseViewModels;
using GymJournal.App.ViewModel.UserInfoViewModels;
using GymJournal.App.ViewModel.WorkoutPlanViewModels;
using GymJournal.App.ViewModel.WorkoutViewModels;
using GymJournal.App.ViewModel.WorkSetViewModels;
using Microsoft.Extensions.Logging;

namespace GymJournal.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddScoped<IExerciseService, ExerciseService>();
		builder.Services.AddScoped<IMuscleService, MuscleService>();
		builder.Services.AddScoped<IUserInfoService, UserInfoService>();
		builder.Services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
		builder.Services.AddScoped<IWorkoutService, WorkoutService>();
		builder.Services.AddScoped<IWorkSetService, WorkSetService>();


		builder.Services.AddSingleton<MainPageViewModel>();

		builder.Services.AddTransient<UserEditPageViewModel>();
		builder.Services.AddTransient<UserListPageViewModel>();
		builder.Services.AddTransient<UserLoginPageViewModel>();
		builder.Services.AddTransient<UserProfilePageViewModel>();
		builder.Services.AddTransient<UserRegisterPageViewModel>();

		builder.Services.AddTransient<ExerciseDetailsPageViewModel>();
		builder.Services.AddTransient<ExerciseListPageViewModel>();
		builder.Services.AddTransient<ExerciseUpsertPageViewModel>();

		builder.Services.AddTransient<WorkoutDetailsPageViewModel>();
		builder.Services.AddTransient<WorkoutListPageViewModel>();
		builder.Services.AddTransient<WorkoutTodayPageViewModel>();
		builder.Services.AddTransient<WorkoutUpsertPageViewModel>();

		builder.Services.AddTransient<WorkoutPlanDetailsPageViewModel>();
		builder.Services.AddTransient<WorkoutPlanListPageViewModel>();
		builder.Services.AddTransient<WorkoutPlanUpsertPageViewModel>();

		builder.Services.AddTransient<WorkSetUpsertPageViewModel>();
		builder.Services.AddTransient<WorkSetListPageViewModel>();


		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddTransient<UserEditPage>();
		builder.Services.AddTransient<UserListPage>();
		builder.Services.AddTransient<UserLoginPage>();
		builder.Services.AddTransient<UserProfilePage>();
		builder.Services.AddTransient<UserRegisterPage>();

		builder.Services.AddTransient<ExerciseDetailsPage>();
		builder.Services.AddTransient<ExerciseListPage>();
		builder.Services.AddTransient<ExerciseUpsertPage>();

		builder.Services.AddTransient<WorkoutDetailsPage>();
		builder.Services.AddTransient<WorkoutListPage>();
		builder.Services.AddTransient<WorkoutTodayPage>();
		builder.Services.AddTransient<WorkoutUpsertPage>();

		builder.Services.AddTransient<WorkoutPlanDetailsPage>();
		builder.Services.AddTransient<WorkoutPlanListPage>();
		builder.Services.AddTransient<WorkoutPlanUpsertPage>();

		builder.Services.AddTransient<WorkSetUpsertPage>();
		builder.Services.AddTransient<WorkSetListPage>();


		builder.Services.AddSingleton<IdentityService>();
		builder.Services.AddTransient<ConstantsService>();
		builder.Services.AddTransient<ExceptionHandlerService>();
		builder.Services.AddTransient<ILocalStorageService, LocalStorageService>();
		builder.Services.AddTransient<IInputValidatorService, InputValidatorService>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
