using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.WorkoutQueries;
using Microsoft.EntityFrameworkCore;
using System;

namespace GymJournal.Data.Repositories
{
	public class WorkoutRepository : IWorkoutRepository
	{
		private readonly IApplicationDbContext _dbContext;

		public WorkoutRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<AddWorkoutResponse> Add(AddWorkoutCommand command, CancellationToken cancellationToken = default)
		{
			var entity = new Workout
			{
				Name = command.Name,
				Description = command.Description,
				Exercises = await _dbContext.Exercises.Where(e => command.ExerciseIds.Contains(e.Id)).ToListAsync(),
				WorkoutPlans = await _dbContext.WorkoutPlans.Where(w => command.WorkoutPlanIds.Contains(w.Id)).ToListAsync(),
			};

			await _dbContext.Workouts.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new AddWorkoutResponse
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

		public async Task Delete(DeleteWorkoutCommand command, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Workouts
				.Include(w => w.Exercises)
				.Include(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(w => w.Id == command.WorkoutId, cancellationToken);

			if (entity == null)
			{
				throw new Exception("The Workout you want to delete does not exist.");
			}

			_dbContext.Workouts.Remove(entity);
			await SaveChanges(cancellationToken);
		}

		public async Task<GetAllWorkoutResponse> GetAll(GetAllWorkoutQuery query, CancellationToken cancellationToken = default)
		{

			var workouts = await _dbContext.Workouts
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

			return new GetAllWorkoutResponse { Workouts = workouts };
		}

		public async Task<GetByIdWorkoutResponse> GetById(GetByIdWorkoutQuery query, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Workouts
				.Include(w => w.Exercises)
				.Include(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(w => w.Id == query.WorkoutId, cancellationToken);

			if (entity == null)
			{
				throw new Exception("The Workout you want to GetById does not exist.");
			}

			return new GetByIdWorkoutResponse
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

		public async Task SaveChanges(CancellationToken cancellationToken = default)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<UpdateWorkoutResponse> Update(UpdateWorkoutCommand command, CancellationToken cancellationToken = default)
		{
			var entityToUpdate = await _dbContext.Workouts
				.Include(w => w.Exercises)
				.Include(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(w => w.Id == command.WorkoutId, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new Exception("The Workout you want to update does not exist.");
			}

			if (command.Name != null) { entityToUpdate.Name = command.Name; }
			if (command.Description != null) { entityToUpdate.Description = command.Description; }
			if (command.ExerciseIds != null)
			{
				entityToUpdate.Exercises =
					await _dbContext.Exercises.Where(e => command.ExerciseIds.Contains(e.Id)).ToListAsync();
			}
			if (command.WorkoutPlanIds != null)
			{
				entityToUpdate.WorkoutPlans =
					await _dbContext.WorkoutPlans.Where(w => command.WorkoutPlanIds.Contains(w.Id)).ToListAsync();
			}

			await SaveChanges(cancellationToken);

			return new UpdateWorkoutResponse
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
