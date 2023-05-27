using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymJournal.Data.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public virtual DbSet<Muscle> Muscles { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
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