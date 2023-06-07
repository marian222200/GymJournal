using GymJournal.Data.Entities;
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
		public DbSet<Muscle> Muscles { get; set; }
		public DbSet<Exercise> Exercises { get; set; }
		public DbSet<Workout> Workouts { get; set; }
		public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
		public DbSet<UserInfo> UserInfos { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
