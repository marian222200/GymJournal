using GymJournal.Data.Entities;
using GymJournal.Data.Entities.ExerciseTypes;
using GymJournal.Data.Entities.WorkoutTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Context.IContext
{
	public interface IApplicationDbContext
	{
		public DbSet<Muscle> MuscleGroups { get; set; }
		public DbSet<Exercise> Exercises { get; set; }
		public DbSet<ExerciseEntry> ExerciseEntries { get; set; }
		public DbSet<PastExercise> PastExercises { get; set; }
		public DbSet<Workout> Workouts { get; set; }
		public DbSet<PastWorkout> PastWorkouts { get; set; }
		public DbSet<WorkoutPlan> WorkoutPlans { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
