using GymJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymJournal.Data.Configuration
{
	public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
	{
		public void Configure(EntityTypeBuilder<UserInfo> builder)
		{
			builder.HasKey(u => u.Id);
			builder.HasIndex(u => u.Name).IsUnique();
			builder.HasData(new UserInfo
			{
				Id = Guid.Parse("42282faf-05a4-48ff-b062-65fed7b5e84a"),
				Name = "Admin",
				Password = BCryptNet.HashPassword("AsdAsd1!"),
				Role = "Admin",
				Token = Guid.Parse("8ae01d7d-3965-4b7e-b8af-e12fd5f588f6"),
				WorkSets = new List<WorkSet>()
			});
		}
	}
}
