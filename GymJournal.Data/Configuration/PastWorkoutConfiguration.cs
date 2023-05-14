using GymJournal.Data.Entities.WorkoutTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Configuration
{
	public class PastWorkoutConfiguration : IEntityTypeConfiguration<PastWorkout>
	{
		public void Configure(EntityTypeBuilder<PastWorkout> builder)
		{
			builder.HasKey(w => w.Id);
			builder
				.HasOne<Workout>(w => w.Workout)
				.WithMany(w => w.PastWorkouts)
				.HasForeignKey(w => w.WorkoutId);
		}
	}
}
