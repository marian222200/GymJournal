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
	public class ExerciseRepositoryUnitTests
	{
		private Mock<IApplicationDbContext> _mockApplicationDbContext = null!;
		private ExerciseRepository _exerciseRepository = null!;

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
			}.AsQueryable().BuildMockDbSet();

			var exerciseDbSetMock = new List<Exercise>
			{
				new Exercise
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "e1",
					Description = "e1d",
					Muscles = new List<Muscle>
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
					},
					Workouts = new List<Workout>
					{
						new Workout
						{
							Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
							Name = "w1",
							Description = "w1d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>()
						},
						new Workout
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "w2",
							Description = "wd2",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>()
						},
					}
				},
				new Exercise
				{
					Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
					Name = "e2",
					Description = "e2d",
					Muscles = new List<Muscle>
					{
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
					},
					Workouts = new List<Workout>
					{
						new Workout
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "w2",
							Description = "wd2",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>()
						},
						new Workout
						{
							Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
							Name = "w3",
							Description = "w3d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>()
						},
					}
				}
			}.AsQueryable().BuildMockDbSet();

			var workoutDbSetMock = new List<Workout>
			{
				new Workout
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "w1",
					Description = "w1d",
					Exercises = new List<Exercise>(),
					WorkoutPlans = new List<WorkoutPlan>()
				},
				new Workout
				{
					Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
					Name = "w2",
					Description = "wd2",
					Exercises = new List<Exercise>(),
					WorkoutPlans = new List<WorkoutPlan>()
				},
				new Workout
				{
					Id = Guid.Parse("5a25a051-75d3-4733-839c-f645ca8f20f7"),
					Name = "w3",
					Description = "w3d",
					Exercises = new List<Exercise>(),
					WorkoutPlans = new List<WorkoutPlan>()
				},
			}.AsQueryable().BuildMockDbSet();

			_mockApplicationDbContext = new Mock<IApplicationDbContext>();
			_mockApplicationDbContext.Setup(c => c.Muscles).Returns(muscleDbSetMock.Object);
			_mockApplicationDbContext.Setup(c => c.Exercises).Returns(exerciseDbSetMock.Object);
			_mockApplicationDbContext.Setup(c => c.Workouts).Returns(workoutDbSetMock.Object);

			_exerciseRepository = new ExerciseRepository(_mockApplicationDbContext.Object);
		}

		[Test]
		public async Task TestThatAddReturnsTheCorrectEntityForValidInput()
		{
			var receivedResponse = await _exerciseRepository.Add(ValidExistentExerciseDto());

			Assert.That(receivedResponse, Is.Not.Null);
			Assert.That(receivedResponse.Name, Is.EqualTo(ValidExistentExerciseDto().Name));
			Assert.That(receivedResponse.Description, Is.EqualTo(ValidExistentExerciseDto().Description));
			Assert.That(receivedResponse.MuscleIds, Is.EqualTo(ValidExistentExerciseDto().MuscleIds));
			Assert.That(receivedResponse.WorkoutIds, Is.EqualTo(ValidExistentExerciseDto().WorkoutIds));
		}

		[Test]
		public async Task TestThatGetAllReturnsAllEntities()
		{
			var receivedResponse = await _exerciseRepository.GetAll();
			receivedResponse.Should().BeEquivalentTo(_mockApplicationDbContext.Object.Exercises
				.Select(entity => new ExerciseDto
				{
					Id = entity.Id,
					Name = entity.Name,
					Description = entity.Description,
					MuscleIds = entity.Muscles.Select(m => m.Id).ToList(),
					WorkoutIds = entity.Workouts.Select(w => w.Id).ToList(),
				}));
		}

		[Test]
		public async Task TestThatGetByIdReturnsTheGoodEntityForValidInput()
		{
			var recievedResponse = await _exerciseRepository
				.GetById(Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"));

			recievedResponse.Should().BeEquivalentTo(ValidExistentExerciseDto());
		}

		[Test]
		public void TestThatGetByIdReturnsErrorForNullGuid()
		{
			Guid? guid = null;

			var thrownException = Assert.ThrowsAsync<ArgumentNullException>(async Task () => await _exerciseRepository
				.GetById(guid));

			var expectedExceptionMessage = new ArgumentNullException(nameof(guid)).Message;

			Assert.That(thrownException.Message, Is.EqualTo(expectedExceptionMessage));
		}

		[Test]
		public void TestThatGetByIdThrowsErrorWhenFirstOrDefaultReturnsNull()
		{
			var id = Guid.Parse("1294a6cd-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _exerciseRepository
				.GetById(id));

			Assert.That(thrownException.Message, Is.EqualTo("The exercise you want to GetById does not exist."));
		}


		[Test]
		public void TestThatRemoveThrowsErrorForNullId()
		{
			Guid? id = null;

			var thrownException = Assert.ThrowsAsync<ArgumentNullException>(async Task () => await _exerciseRepository
				.Remove(id));

			var expectedExceptionMessage = new ArgumentNullException(nameof(id)).Message;

			Assert.That(thrownException.Message, Is.EqualTo(expectedExceptionMessage));
		}

		[Test]
		public void TestThatRemoveThrowsErrorWhenFirstOrDefaultReturnsNull()
		{
			var id = Guid.Parse("1294a6cd-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _exerciseRepository
				.Remove(id));

			Assert.That(thrownException.Message, Is.EqualTo("The exercise you want to delete does not exist."));
		}

		[Test]
		public async Task TestThatRemoveCallsForDbContextRemoveForValidInput()
		{
			await _exerciseRepository.Remove(Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"));
			_mockApplicationDbContext.Verify(
				x => x.Exercises.Remove(It.IsAny<Exercise>()), Times.Once);

		}

		[Test]
		public async Task TestThatSaveChangesCallsSaveChangesAsyncOnDbContext()
		{
			await _exerciseRepository.SaveChanges();
			_mockApplicationDbContext.Verify(
				x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Test]
		public async Task TestThatUpdateThrowsErrorForInexistentItem()
		{
			var testItem = ValidExistentExerciseDto();
			testItem.Id = Guid.Parse("1294b6aa-ea68-4fa1-96f0-7362eca5da0d");

			var thrownException = Assert.ThrowsAsync<ArgumentException>(async Task () => await _exerciseRepository
				.Update(testItem));

			Assert.That(thrownException.Message, Is.EqualTo("The exercise you want to update does not exist."));
		}

		[Test]
		public async Task TestThatUpdateUpdatesCorrectEntityAndReturnsRightDtoOnValidInput()
		{
			var testItem = ValidExistentExerciseDto();
			testItem.Name = "TEST";

			var receivedResponse = await _exerciseRepository.Update(testItem);

			_mockApplicationDbContext.Object.Exercises
				.Should().ContainEquivalentOf(new Exercise
				{
					Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Name = "TEST",
					Description = "e1d",
					Muscles = new List<Muscle>
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
					},
					Workouts = new List<Workout>
					{
						new Workout
						{
							Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
							Name = "w1",
							Description = "w1d",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>()
						},
						new Workout
						{
							Id = Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
							Name = "w2",
							Description = "wd2",
							Exercises = new List<Exercise>(),
							WorkoutPlans = new List<WorkoutPlan>()
						},
					}
				});

			receivedResponse.Should().BeEquivalentTo(testItem);
		}

		[TearDown]
		public void TearDown()
		{
			_mockApplicationDbContext = null!;
			_exerciseRepository = null!;
		}

		private ExerciseDto ValidExistentExerciseDto()
		{
			return new ExerciseDto
			{
				Id = Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
				Name = "e1",
				Description = "e1d",
				MuscleIds = new List<Guid>
				{
					Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
				},
				WorkoutIds = new List<Guid>
				{
					Guid.Parse("3cf1dc75-d641-4305-97e5-eb629f454676"),
					Guid.Parse("f62031db-81a1-4643-8e44-845c19eb51e4"),
				},
			};
		}
	}
}
