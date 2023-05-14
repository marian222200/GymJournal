using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Data.Entities.ExerciseTypes;
using GymJournal.Data.Entities.WorkoutTypes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymJournal.Data.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public virtual DbSet<Muscle> MuscleGroups { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<ExerciseEntry> ExerciseEntries { get; set; }
        public virtual DbSet<PastExercise> PastExercises { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<PastWorkout> PastWorkouts { get; set; }
        public virtual DbSet<WorkoutPlan> WorkoutPlans { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
			options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}