﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymJournal.Data.Entities;

namespace GymJournal.Data.Configuration
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
	{
		public void Configure(EntityTypeBuilder<Workout> builder)
		{
			builder.HasKey(w => w.Id);
			builder
				.HasMany(w => w.Exercises)
				.WithMany(e => e.Workouts);
		}
	}
}
