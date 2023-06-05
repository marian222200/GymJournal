using GymJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Configuration
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
	{
		public void Configure(EntityTypeBuilder<Exercise> builder)
		{
			builder.HasKey(e => e.Id);
			builder
				.HasMany(e => e.Muscles)
				.WithMany(m => m.Exercises);
		}
	}
}
