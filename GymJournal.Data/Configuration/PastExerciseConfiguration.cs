using GymJournal.Data.Entities.ExerciseTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Configuration
{
	public class PastExerciseConfiguration : IEntityTypeConfiguration<PastExercise>
	{
		public void Configure(EntityTypeBuilder<PastExercise> builder)
		{
			builder.HasKey(e => e.Id);
			builder
				.HasOne<Exercise>(e => e.Exercise)
				.WithMany(e => e.PastExercises)
				.HasForeignKey(e => e.ExerciseId);
		}
	}
}
