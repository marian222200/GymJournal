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
	public class WorkSetConfiguration : IEntityTypeConfiguration<WorkSet>
	{
		public void Configure(EntityTypeBuilder<WorkSet> builder)
		{
			builder.HasKey(x => x.Id);
			builder
				.HasOne(w => w.User)
				.WithMany(u => u.WorkSets);
			builder
				.HasOne(w => w.Exercise)
				.WithMany(e => e.WorkSets);
		}
	}
}
