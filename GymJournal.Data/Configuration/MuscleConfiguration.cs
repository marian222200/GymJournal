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
	public class MuscleConfiguration : IEntityTypeConfiguration<Muscle>
	{
		public void Configure(EntityTypeBuilder<Muscle> builder)
		{
			builder.HasKey(m => m.Id);
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
				Name = "Chest",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
				Name = "Triceps",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
				Name = "Back",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("3c0b0369-e88c-40f0-bc30-a1a309958db4"),
				Name = "Biceps",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("909c35a3-a2c0-4fc9-97b6-5299a0d88dab"),
				Name = "Forearms",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("f46d7cf7-a3a8-42eb-97c5-0e3b10b5bbe1"),
				Name = "Abs",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("8bd9fdf8-f0b5-4361-b447-b926195637c7"),
				Name = "FrontalDelt",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("576d342b-b493-4edc-ad56-fffd2ffa36fb"),
				Name = "LateralDelt",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("6328df6e-3f56-4ad6-ba7b-50ab92270b48"),
				Name = "RearDelt",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("3ef37065-e38e-4676-a0da-af322e750d45"),
				Name = "Quads",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("dc0d5865-8fa7-48a1-89cc-65986e10928e"),
				Name = "Hamstrings",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("a2d5742d-a68a-4b70-a7ac-94f31dd77b03"),
				Name = "Calves",
			});
			builder.HasData(new Muscle
			{
				Id = Guid.Parse("a8627c30-a7b9-4b2c-af13-350b77faa36b"),
				Name = "Other",
			});
		}
	}
}
