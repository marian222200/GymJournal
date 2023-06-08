using GymJournal.API.Controllers;
using GymJournal.Data.Context;
using GymJournal.Data.Context.IContext;
using GymJournal.Data.Repositories;
using GymJournal.Data.RequestValidators;
using GymJournal.Data.RequestValidators.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymJournal.API
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString,
					optionsBuilder => optionsBuilder.MigrationsAssembly("GymJournal.Data")));

			builder.Services.AddOptions();

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
			builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
			builder.Services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
			builder.Services.AddScoped<IMuscleRepository, MuscleRepository>();
			builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();

			builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
			builder.Services.AddScoped<ApplicationDbContextInitializer>();


			builder.Services.AddScoped<IValidationAuthorization, ValidationAuthorization>();
			builder.Services.AddScoped<ExceptionHandler>();


			builder.Services.AddScoped<IMuscleValidators, MuscleValidators>();
			builder.Services.AddScoped<IExerciseValidators, ExerciseValidators>();
			builder.Services.AddScoped<IWorkoutValidators, WorkoutValidators>();
			builder.Services.AddScoped<IWorkoutPlanValidators, WorkoutPlanValidators>();
			builder.Services.AddScoped<IUserInfoValidators, UserInfoValidators>();


			var app = builder.Build();

			// Initialise and seed database
			using var scope = app.Services.CreateScope();
			var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
			await initializer.InitialiseAsync();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseCors();
			
			app.UseRouting();

			app.MapControllers();

			app.Run();
		}
	}
}