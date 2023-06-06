using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GymJournal.Data.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
	{
		private readonly IApplicationDbContext _dbContext;

		public WorkoutRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}
		public async Task<WorkoutDto> Add(AddWorkoutCommand command, CancellationToken cancellationToken = default)
		{
			var entity = new Workout
			{
				//Id = Guid.NewGuid(),
				Name = command.Name,
				Description = command.Description,
				Exercises = await _dbContext.Exercises.Where(e => command.ExerciseIds.Contains(e.Id)).ToListAsync(),
				WorkoutPlans = await _dbContext.WorkoutPlans.Where(w => command.WorkoutPlanIds.Contains(w.Id)).ToListAsync(),
			};

			await _dbContext.Workouts.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new WorkoutDto
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				Exercises = entity.Exercises.Select(e => new ExerciseDto
				{
					Id = e.Id,
					Name = e.Name,
					Description = e.Description,
					Likes = e.Likes,
					Muscles = new List<MuscleDto>(),
					Workouts = new List<WorkoutDto>(),
				}).ToList(),
				WorkoutPlans = entity.WorkoutPlans.Select(w => new WorkoutPlanDto
				{
					Id = w.Id,
					Name = w.Name,
					Description = w.Description,
					Workouts = new List<WorkoutDto>(),
				}).ToList(),
			};
		}

		public async Task<IEnumerable<WorkoutDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var dtos = await _dbContext.Workouts
				.AsNoTracking()
				.Select(entity => new WorkoutDto
				{
					Id = entity.Id,
					Name = entity.Name,
					Description = entity.Description,
					Exercises = entity.Exercises.Select(e => new ExerciseDto
					{
						Id = e.Id,
						Name = e.Name,
						Description = e.Description,
						Likes = e.Likes,
						Muscles = new List<MuscleDto>(),
						Workouts = new List<WorkoutDto>(),
					}).ToList(),
					WorkoutPlans = entity.WorkoutPlans.Select(w => new WorkoutPlanDto
					{
						Id = w.Id,
						Name = w.Name,
						Description = w.Description,
						Workouts = new List<WorkoutDto>(),
					}).ToList(),
				})
				.ToListAsync(cancellationToken);

			return dtos;
		}

		public async Task<WorkoutDto?> GetById(Guid? guid, CancellationToken cancellationToken = default)
		{
			if (guid == null)
			{
				throw new ArgumentNullException(nameof(guid));
			}

			var entity = await _dbContext.Workouts
				.Include(w => w.Exercises)
				.Include(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(w => w.Id == guid, cancellationToken);

			if (entity == null)
			{
				throw new ArgumentException("The Workout you want to GetById does not exist.");
			}

			return new WorkoutDto
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				Exercises = entity.Exercises.Select(e => new ExerciseDto
				{
					Id = e.Id,
					Name = e.Name,
					Description = e.Description,
					Likes = e.Likes,
					Muscles = new List<MuscleDto>(),
					Workouts = new List<WorkoutDto>(),
				}).ToList(),
				WorkoutPlans = entity.WorkoutPlans.Select(w => new WorkoutPlanDto
				{
					Id = w.Id,
					Name = w.Name,
					Description = w.Description,
					Workouts = new List<WorkoutDto>(),
				}).ToList(),
			};
		}

		public async Task Remove(Guid? id, CancellationToken cancellationToken = default)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			var entity = await _dbContext.Workouts
				.Include(w => w.Exercises)
				.Include(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

			if (entity == null)
			{
				throw new ArgumentException("The Workout you want to delete does not exist.");
			}

			_dbContext.Workouts.Remove(entity);
			await SaveChanges(cancellationToken);
		}

		public async Task SaveChanges(CancellationToken cancellationToken = default)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<WorkoutDto> Update(UpdateWorkoutCommand command, CancellationToken cancellationToken = default)
		{
			var entity = new Workout
			{
				Id = command.Id,
				Name = command.Name,
				Description = command.Description,
				Exercises = await _dbContext.Exercises.Where(e => command.ExerciseIds.Contains(e.Id)).ToListAsync(),
				WorkoutPlans = await _dbContext.WorkoutPlans.Where(w => command.WorkoutPlanIds.Contains(w.Id)).ToListAsync(),
			};

			var entityToUpdate = await _dbContext.Workouts
				.Include(w => w.Exercises)
				.Include(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(w => w.Id == entity.Id, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new ArgumentException("The Workout you want to update does not exist.");
			}

			entityToUpdate.Name = entity.Name;
			entityToUpdate.Description = entity.Description;
			entityToUpdate.Exercises = entity.Exercises;
			entityToUpdate.WorkoutPlans = entity.WorkoutPlans;

			await SaveChanges(cancellationToken);

			return new WorkoutDto
			{
				Id = entityToUpdate.Id,
				Name = entityToUpdate.Name,
				Description = entityToUpdate.Description,
				Exercises = entityToUpdate.Exercises.Select(e => new ExerciseDto
				{
					Id = e.Id,
					Name = e.Name,
					Description = e.Description,
					Likes = e.Likes,
					Muscles = new List<MuscleDto>(),
					Workouts = new List<WorkoutDto>(),
				}).ToList(),
				WorkoutPlans = entityToUpdate.WorkoutPlans.Select(w => new WorkoutPlanDto
				{
					Id = w.Id,
					Name = w.Name,
					Description = w.Description,
					Workouts = new List<WorkoutDto>(),
				}).ToList(),
			};
		}
	}
}
