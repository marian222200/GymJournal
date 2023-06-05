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
	public class WorkoutRepositoryUnitTests
	{
		private Mock<IApplicationDbContext> _mockApplicationDbContext = null!;
		private WorkoutRepository _workoutRepository = null!;

		[SetUp]
		public void Setup()
		{
			var exerciseDbSetMock = new List<Exercise>
			{
				new Exercise
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "e1",
					Description = "e1d",
					Muscles = new List<Muscle>(),
					Workouts = new List<Workout>(),
				},
				new Exercise
				{
					Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
					Name = "e2",
					Description = "e2d",
					Muscles = new List<Muscle>(),
					Workouts = new List<Workout>(),
				},
				new Exercise
				{
					Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
					Name = "e3",
					Description = "e3d",
					Muscles = new List<Muscle>(),
					Workouts = new List<Workout>(),
				},
			}.AsQueryable().BuildMockDbSet();

			var workoutDbSetMock = new List<Workout>
			{
				new Workout
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "w1",
					Description = "w1d",
					Exercises = new List<Exercise>
					{
						new Exercise
						{
							Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
							Name = "e1",
							Description = "e1d",
							Muscles = new List<Muscle>(),
							Workouts = new List<Workout>(),
						},
						new Exercise
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "e2",
							Description = "e2d",
							Muscles = new List<Muscle>(),
							Workouts = new List<Workout>(),
						},
					},
					WorkoutPlans = new List<WorkoutPlan>
					{
						new WorkoutPlan
						{
							Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
							Name = "wp1",
							Description = "wp1d",
							Workouts = new List<Workout>(),
						},
						new WorkoutPlan
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "wp2",
							Description = "wp2d",
							Workouts = new List<Workout>(),
						}
					}
				},
				new Workout
				{
					Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
					Name = "w2",
					Description = "w2d",
					Exercises = new List<Exercise>
					{
						new Exercise
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "e2",
							Description = "e2d",
							Muscles = new List<Muscle>(),
							Workouts = new List<Workout>(),
						},
						new Exercise
						{
							Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
							Name = "e3",
							Description = "e3d",
							Muscles = new List<Muscle>(),
							Workouts = new List<Workout>(),
						},
					},
					WorkoutPlans = new List<WorkoutPlan>
					{
						new WorkoutPlan
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "wp2",
							Description = "wp2d",
							Workouts = new List<Workout>(),
						},
						new WorkoutPlan
						{
							Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
							Name = "wp3",
							Description = "wp3d",
							Workouts = new List<Workout>(),
						},
					},
				},
			}.AsQueryable().BuildMockDbSet();

			var workoutPlansDbSetMock = new List<WorkoutPlan>
			{
				new WorkoutPlan
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "wp1",
					Description = "wp1d",
					Workouts = new List<Workout>(),
				},
				new WorkoutPlan
				{
					Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
					Name = "wp2",
					Description = "wp2d",
					Workouts = new List<Workout>(),
				},
				new WorkoutPlan
				{
					Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
					Name = "wp3",
					Description = "wp3d",
					Workouts = new List<Workout>(),
				},
			}.AsQueryable().BuildMockDbSet();

			_mockApplicationDbContext = new Mock<IApplicationDbContext>();
			_mockApplicationDbContext.Setup(c => c.Exercises).Returns(exerciseDbSetMock.Object);
			_mockApplicationDbContext.Setup(c => c.Workouts).Returns(workoutDbSetMock.Object);
			_mockApplicationDbContext.Setup(c => c.WorkoutPlans).Returns(workoutPlansDbSetMock.Object);

			_workoutRepository = new WorkoutRepository(_mockApplicationDbContext.Object);
		}

		[Test]
		public async Task TestThatAddReturnsTheCorrectEntityForValidInput()
		{
			var receivedResponse = await _workoutRepository.Add(ValidExistentWorkoutDto());

			Assert.That(receivedResponse, Is.Not.Null);
			Assert.That(receivedResponse.Name, Is.EqualTo(ValidExistentWorkoutDto().Name));
			Assert.That(receivedResponse.Description, Is.EqualTo(ValidExistentWorkoutDto().Description));
			Assert.That(receivedResponse.ExerciseIds, Is.EqualTo(ValidExistentWorkoutDto().ExerciseIds));
			Assert.That(receivedResponse.WorkoutPlanIds, Is.EqualTo(ValidExistentWorkoutDto().WorkoutPlanIds));
		}

		[Test]
		public async Task TestThatGetAllReturnsAllEntities()
		{
			var receivedResponse = await _workoutRepository.GetAll();
			receivedResponse.Should().BeEquivalentTo(_mockApplicationDbContext.Object.Workouts
				.Select(entity => new WorkoutDto
				{
					Id = entity.Id,
					Name = entity.Name,
					Description = entity.Description,
					ExerciseIds = entity.Exercises.Select(w => w.Id).ToList(),
					WorkoutPlanIds = entity.WorkoutPlans.Select(w => w.Id).ToList(),
				}));
		}

		[Test]
		public async Task TestThatGetByIdReturnsTheGoodEntityForValidInput()
		{
			var recievedResponse = await _workoutRepository
				.GetById(Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"));

			recievedResponse.Should().BeEquivalentTo(ValidExistentWorkoutDto());
		}

		[Test]
		public void TestThatGetByIdReturnsErrorForNullGuid()
		{
			Guid? guid = null;

			var thrownException = Assert.ThrowsAsync<ArgumentNullException>(async Task () => await _workoutRepository
				.GetById(guid));

			var expectedExceptionMessage = new ArgumentNullException(nameof(guid)).Message;

			Assert.That(thrownException.Message, Is.EqualTo(expectedExceptionMessage));
		}

		[Test]
		public void TestThatGetByIdThrowsErrorWhenFirstOrDefaultReturnsNull()
		{
			var id = Guid.Parse("1294a6cd-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _workoutRepository
				.GetById(id));

			Assert.That(thrownException.Message, Is.EqualTo("The Workout you want to GetById does not exist."));
		}


		[Test]
		public void TestThatRemoveThrowsErrorForNullId()
		{
			Guid? id = null;

			var thrownException = Assert.ThrowsAsync<ArgumentNullException>(async Task () => await _workoutRepository
				.Remove(id));

			var expectedExceptionMessage = new ArgumentNullException(nameof(id)).Message;

			Assert.That(thrownException.Message, Is.EqualTo(expectedExceptionMessage));
		}

		[Test]
		public void TestThatRemoveThrowsErrorWhenFirstOrDefaultReturnsNull()
		{
			var id = Guid.Parse("1294a6cd-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _workoutRepository
				.Remove(id));

			Assert.That(thrownException.Message, Is.EqualTo("The Workout you want to delete does not exist."));
		}

		[Test]
		public async Task TestThatRemoveCallsForDbContextRemoveForValidInput()
		{
			await _workoutRepository.Remove(Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"));
			_mockApplicationDbContext.Verify(
				x => x.Workouts.Remove(It.IsAny<Workout>()), Times.Once);

		}

		[Test]
		public async Task TestThatSaveChangesCallsSaveChangesAsyncOnDbContext()
		{
			await _workoutRepository.SaveChanges();
			_mockApplicationDbContext.Verify(
				x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Test]
		public async Task TestThatUpdateThrowsErrorForInexistentItem()
		{
			var testItem = ValidExistentWorkoutDto();
			testItem.Id = Guid.Parse("1294b6aa-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _workoutRepository
				.Update(testItem));

			Assert.That(thrownException.Message, Is.EqualTo("The Workout you want to update does not exist."));
		}

		[Test]
		public async Task TestThatUpdateUpdatesCorrectEntityAndReturnsRightDtoOnValidInput()
		{
			var testItem = ValidExistentWorkoutDto();
			testItem.Name = "TEST";

			var receivedResponse = await _workoutRepository.Update(testItem);

			_mockApplicationDbContext.Object.Workouts
				.Should().ContainEquivalentOf(new Workout
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "TEST",
					Description = "w1d",
					Exercises = new List<Exercise>
					{
						new Exercise
						{
							Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
							Name = "e1",
							Description = "e1d",
							Muscles = new List<Muscle>(),
							Workouts = new List<Workout>(),
						},
						new Exercise
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "e2",
							Description = "e2d",
							Muscles = new List<Muscle>(),
							Workouts = new List<Workout>(),
						},
					},
					WorkoutPlans = new List<WorkoutPlan>
					{
						new WorkoutPlan
						{
							Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
							Name = "wp1",
							Description = "wp1d",
							Workouts = new List<Workout>(),
						},
						new WorkoutPlan
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "wp2",
							Description = "wp2d",
							Workouts = new List<Workout>(),
						}
					}
				});

			receivedResponse.Should().BeEquivalentTo(testItem);
		}

		[TearDown]
		public void TearDown()
		{
			_mockApplicationDbContext = null!;
			_workoutRepository = null!;
		}

		private WorkoutDto ValidExistentWorkoutDto()
		{
			return new WorkoutDto
			{
				Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
				Name = "w1",
				Description = "w1d",
				ExerciseIds = new List<Guid>
				{
					Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
				},
				WorkoutPlanIds = new List<Guid>
				{
					Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
				},
			};
		}
	}
}
