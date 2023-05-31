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
	public class WorkoutPlanRepositoryUnitTests
	{
		private Mock<IApplicationDbContext> _mockApplicationDbContext = null!;
		private WorkoutPlanRepository _workoutPlanRepository = null!;

		[SetUp]
		public void Setup()
		{
			var workoutDbSetMock = new List<Workout>
			{
				new Workout
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "w1",
					Description = "w1d",
					Exercises = new List<Exercise>(),
					WorkoutPlans = new List<WorkoutPlan>(),
				},
				new Workout
				{
					Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
					Name = "w2",
					Description = "w2d",
					Exercises = new List<Exercise>(),
					WorkoutPlans = new List<WorkoutPlan>(),
				},
				new Workout
				{
					Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
					Name = "w3",
					Description = "w3d",
					Exercises = new List<Exercise>(),
					WorkoutPlans = new List<WorkoutPlan>(),
				},
			}.AsQueryable().BuildMockDbSet();

			var workoutPlanDbSetMock = new List<WorkoutPlan>
			{
				new WorkoutPlan
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "wp1",
					Description = "wp1d",
					Workouts = new List<Workout>
					{
						new Workout
						{
							Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
							Name = "w1",
							Description = "w1d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>(),
						},
						new Workout
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "w2",
							Description = "w2d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>(),
						},
					}
				},
				new WorkoutPlan
				{
					Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
					Name = "wp2",
					Description = "wp2d",
					Workouts = new List<Workout>
					{
						new Workout
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "w2",
							Description = "w2d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>(),
						},
						new Workout
						{
							Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
							Name = "w3",
							Description = "w3d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>(),
						},
					}
				},
			}.AsQueryable().BuildMockDbSet();

			_mockApplicationDbContext = new Mock<IApplicationDbContext>();
			_mockApplicationDbContext.Setup(c => c.Workouts).Returns(workoutDbSetMock.Object);
			_mockApplicationDbContext.Setup(c => c.WorkoutPlans).Returns(workoutPlanDbSetMock.Object);

			_workoutPlanRepository = new WorkoutPlanRepository(_mockApplicationDbContext.Object);
		}

		[Test]
		public async Task TestThatAddReturnsTheCorrectEntityForValidInput()
		{
			var receivedResponse = await _workoutPlanRepository.Add(ValidExistentWorkoutPlanDto());

			Assert.That(receivedResponse, Is.Not.Null);
			Assert.That(receivedResponse.Name, Is.EqualTo(ValidExistentWorkoutPlanDto().Name));
			Assert.That(receivedResponse.Description, Is.EqualTo(ValidExistentWorkoutPlanDto().Description));
			Assert.That(receivedResponse.WorkoutIds, Is.EqualTo(ValidExistentWorkoutPlanDto().WorkoutIds));
		}

		[Test]
		public async Task TestThatGetAllReturnsAllEntities()
		{
			var receivedResponse = await _workoutPlanRepository.GetAll();
			receivedResponse.Should().BeEquivalentTo(_mockApplicationDbContext.Object.WorkoutPlans
				.Select(entity => new WorkoutPlanDto
				{
					Id = entity.Id,
					Name = entity.Name,
					Description = entity.Description,
					WorkoutIds = entity.Workouts.Select(w => w.Id).ToList(),
				}));
		}

		[Test]
		public async Task TestThatGetByIdReturnsTheGoodEntityForValidInput()
		{
			var recievedResponse = await _workoutPlanRepository
				.GetById(Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"));

			recievedResponse.Should().BeEquivalentTo(ValidExistentWorkoutPlanDto());
		}

		[Test]
		public void TestThatGetByIdReturnsErrorForNullGuid()
		{
			Guid? guid = null;

			var thrownException = Assert.ThrowsAsync<ArgumentNullException>(async Task () => await _workoutPlanRepository
				.GetById(guid));

			var expectedExceptionMessage = new ArgumentNullException(nameof(guid)).Message;

			Assert.That(thrownException.Message, Is.EqualTo(expectedExceptionMessage));
		}

		[Test]
		public void TestThatGetByIdThrowsErrorWhenFirstOrDefaultReturnsNull()
		{
			var id = Guid.Parse("1294a6cd-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _workoutPlanRepository
				.GetById(id));

			Assert.That(thrownException.Message, Is.EqualTo("The WorkoutPlan you want to GetById does not exist."));
		}


		[Test]
		public void TestThatRemoveThrowsErrorForNullId()
		{
			Guid? id = null;

			var thrownException = Assert.ThrowsAsync<ArgumentNullException>(async Task () => await _workoutPlanRepository
				.Remove(id));

			var expectedExceptionMessage = new ArgumentNullException(nameof(id)).Message;

			Assert.That(thrownException.Message, Is.EqualTo(expectedExceptionMessage));
		}

		[Test]
		public void TestThatRemoveThrowsErrorWhenFirstOrDefaultReturnsNull()
		{
			var id = Guid.Parse("1294a6cd-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _workoutPlanRepository
				.Remove(id));

			Assert.That(thrownException.Message, Is.EqualTo("The WorkoutPlan you want to delete does not exist."));
		}

		[Test]
		public async Task TestThatRemoveCallsForDbContextRemoveForValidInput()
		{
			await _workoutPlanRepository.Remove(Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"));
			_mockApplicationDbContext.Verify(
				x => x.WorkoutPlans.Remove(It.IsAny<WorkoutPlan>()), Times.Once);

		}

		[Test]
		public async Task TestThatSaveChangesCallsSaveChangesAsyncOnDbContext()
		{
			await _workoutPlanRepository.SaveChanges();
			_mockApplicationDbContext.Verify(
				x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Test]
		public async Task TestThatUpdateThrowsErrorForInexistentItem()
		{
			var testItem = ValidExistentWorkoutPlanDto();
			testItem.Id = Guid.Parse("1294b6aa-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _workoutPlanRepository
				.Update(testItem));

			Assert.That(thrownException.Message, Is.EqualTo("The workoutPlan you want to update does not exist."));
		}

		[Test]
		public async Task TestThatUpdateUpdatesCorrectEntityAndReturnsRightDtoOnValidInput()
		{
			var testItem = ValidExistentWorkoutPlanDto();
			testItem.Name = "TEST";

			var receivedResponse = await _workoutPlanRepository.Update(testItem);

			_mockApplicationDbContext.Object.WorkoutPlans
				.Should().ContainEquivalentOf(new WorkoutPlan
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "TEST",
					Description = "wp1d",
					Workouts = new List<Workout>
					{
						new Workout
						{
							Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
							Name = "w1",
							Description = "w1d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>(),
						},
						new Workout
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "w2",
							Description = "w2d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>(),
						},
					}
				});

			receivedResponse.Should().BeEquivalentTo(testItem);
		}

		[TearDown]
		public void TearDown()
		{
			_mockApplicationDbContext = null!;
			_workoutPlanRepository = null!;
		}

		private WorkoutPlanDto ValidExistentWorkoutPlanDto()
		{
			return new WorkoutPlanDto
			{
				Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
				Name = "wp1",
				Description = "wp1d",
				WorkoutIds = new List<Guid>
				{
					Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
				},
			};
		}
	}
}
