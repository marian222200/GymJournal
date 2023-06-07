using GymJournal.App.Services;
using GymJournal.App.Services.API;
using GymJournal.App.View;
using GymJournal.App.ViewModel;
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

		builder.Services.AddSingleton<MainPageViewModel>();

		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddSingleton<IdentityService>();
		builder.Services.AddSingleton<ConstantsService>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
