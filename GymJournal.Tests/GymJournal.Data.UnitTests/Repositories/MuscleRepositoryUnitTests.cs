using FluentAssertions;
using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Data.Repositories;
using GymJournal.Domain.DTOs;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.UnitTests.Repositories
{
	public class MuscleRepositoryUnitTests
	{
		private Mock<IApplicationDbContext> _mockApplicationDbContext = null!;
		private MuscleRepository _muscleRepoitory = null!;

		[SetUp]
		public void Setup()
		{
			var muscleDbSetMock = new List<Muscle>
			{
				new Muscle
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "Chest",
					Exercises = new List<Exercise>(),
				},
				new Muscle
				{
					Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
					Name = "Triceps",
					Exercises = new List<Exercise>(),
				},
				new Muscle
				{
					Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
					Name = "Back",
					Exercises = new List<Exercise>(),
				},
				new Muscle
				{
					Id = Guid.Parse("3c0b0369-e88c-40f0-bc30-a1a309958db4"),
					Name = "Biceps",
					Exercises = new List<Exercise>(),
				}
			}.AsQueryable().BuildMockDbSet();

			_mockApplicationDbContext = new Mock<IApplicationDbContext>();
			_mockApplicationDbContext.Setup(c => c.Muscles).Returns(muscleDbSetMock.Object);

			_muscleRepoitory = new MuscleRepository(_mockApplicationDbContext.Object);
		}

		[Test]
		public async Task TestThatGetAllReturnsAllEntities()
		{
			var receivedResponse = await _muscleRepoitory.GetAll();
			receivedResponse.Should().BeEquivalentTo(_mockApplicationDbContext.Object.Muscles
				.Select(entity => new MuscleDto
				{
					Id = entity.Id,
					Name = entity.Name,
					ExerciseIds = entity.Exercises.Select(e => e.Id).ToList(),
				}));
		}

		[Test]
		public async Task TestThatGetByIdReturnsTheGoodEntity()
		{
			var recievedResponse = await _muscleRepoitory
				.GetById(Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"));

			recievedResponse.Should().BeEquivalentTo(new MuscleDto
			{
				Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
				Name = "Chest",
				ExerciseIds = new List<Guid> { }
			});
		}

		[TearDown]
		public void TearDown()
		{
			_mockApplicationDbContext = null!;
			_muscleRepoitory = null!;
		}
	}
}
