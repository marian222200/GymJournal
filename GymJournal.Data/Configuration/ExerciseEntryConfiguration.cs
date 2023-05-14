using GymJournal.Data.Entities.ExerciseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Configuration
{
	public class ExerciseEntryConfiguration : IEntityTypeConfiguration<ExerciseEntry>
	{
		public void Configure(EntityTypeBuilder<ExerciseEntry> builder)
		{
			builder.HasKey(e => e.Id);
			builder
				.HasOne<Exercise>(e => e.Exercise)
				.WithMany(e => e.ExerciseEntries)
				.HasForeignKey(e => e.ExerciseId);
		}
	}
}
