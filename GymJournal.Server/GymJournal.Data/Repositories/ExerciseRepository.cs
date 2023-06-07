using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.ExerciseQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public class ExerciseRepository : IExerciseRepository
	{
		private readonly IApplicationDbContext _dbContext;

		public ExerciseRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<AddExerciseResponse> Add(AddExerciseCommand command, CancellationToken cancellationToken = default)
		{
			var entity = new Exercise
			{
				Name = command.Name,
				Description = command.Description,
				Likes = 0,
				Muscles = await _dbContext.Muscles.Where(m => command.MuscleIds.Contains(m.Id)).ToListAsync(),
				Workouts = await _dbContext.Workouts.Where(w => command.WorkoutIds.Contains(w.Id)).ToListAsync(),
			};

			await _dbContext.Exercises.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new AddExerciseResponse
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				Likes = entity.Likes,
				Muscles = entity.Muscles.Select(m => new MuscleDto
				{
					Id = m.Id,
					Name = m.Name,
					Exercises = new List<ExerciseDto>(),
				}).ToList(),
				Workouts = entity.Workouts.Select(w => new WorkoutDto
				{
					Id = w.Id,
					Name = w.Name,
					Description = w.Description,
					Exercises = new List<ExerciseDto>(),
					WorkoutPlans = new List<WorkoutPlanDto>(),
				}).ToList(),
			};
		}

		public async Task Delete(DeleteExerciseCommand command, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Exercises
				.Include(e => e.Muscles)
				.Include(e => e.Workouts)
				.FirstOrDefaultAsync(e => e.Id == command.ExerciseId, cancellationToken);

			if (entity == null)
			{
				throw new Exception("The exercise you want to delete does not exist.");
			}

			_dbContext.Exercises.Remove(entity);
			await SaveChanges(cancellationToken);
		}

		public async Task<GetAllExerciseResponse> GetAll(GetAllExerciseQuery query, CancellationToken cancellationToken = default)
		{
			var exercises = await _dbContext.Exercises
				.AsNoTracking()
				.Select(entity => new ExerciseDto
				{
					Id = entity.Id,
					Name = entity.Name,
					Description = entity.Description,
					Likes = entity.Likes,
					Muscles = entity.Muscles.Select(m => new MuscleDto
					{
						Id = m.Id,
						Name = m.Name,
						Exercises = new List<ExerciseDto>(),
					}).ToList(),
					Workouts = entity.Workouts.Select(w => new WorkoutDto
					{
						Id = w.Id,
						Name = w.Name,
						Description = w.Description,
						Exercises = new List<ExerciseDto>(),
						WorkoutPlans = new List<WorkoutPlanDto>(),
					}).ToList(),
				})
				.ToListAsync(cancellationToken);

			return new GetAllExerciseResponse { Exercises = exercises };
		}

		public async Task<GetByIdExerciseResponse> GetById(GetByIdExerciseQuery query, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Exercises
				.Include(e => e.Muscles)
				.Include(e => e.Workouts)
				.FirstOrDefaultAsync(e => e.Id == query.ExerciseId, cancellationToken);

			if (entity == null)
			{
				throw new Exception("The exercise you want to GetById does not exist.");
			}

			var x = new GetByIdExerciseResponse
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				Likes = entity.Likes,
				Muscles = entity.Muscles.Select(m => new MuscleDto
				{
					Id = m.Id,
					Name = m.Name,
					Exercises = new List<ExerciseDto>(),
				}).ToList(),
				Workouts = entity.Workouts.Select(w => new WorkoutDto
				{
					Id = w.Id,
					Name = w.Name,
					Description = w.Description,
					Exercises = new List<ExerciseDto>(),
					WorkoutPlans = new List<WorkoutPlanDto>(),
				}).ToList(),
			};

			return x;
		}

		public async Task SaveChanges(CancellationToken cancellationToken = default)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<UpdateExerciseResponse> Update(UpdateExerciseCommand command, CancellationToken cancellationToken = default)
		{
			var entityToUpdate = await _dbContext.Exercises
				.Include(e => e.Muscles)
				.Include(e => e.Workouts)
				.FirstOrDefaultAsync(e => e.Id == command.ExerciseId, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new Exception("The exercise you want to update does not exist.");
			}

			if (command.Name != null) { entityToUpdate.Name = command.Name; }
			if (command.Description != null) { entityToUpdate.Description = command.Description; }
			if (command.Likes != null) { entityToUpdate.Likes = command.Likes.Value; }
			if (command.MuscleIds != null) { entityToUpdate.Muscles = 
					await _dbContext.Muscles.Where(m => command.MuscleIds.Contains(m.Id)).ToListAsync(); }
			if (command.WorkoutIds != null) { entityToUpdate.Workouts = 
					await _dbContext.Workouts.Where(w => command.WorkoutIds.Contains(w.Id)).ToListAsync(); }

			await SaveChanges(cancellationToken);

			return new UpdateExerciseResponse
			{
				Id = entityToUpdate.Id,
				Name = entityToUpdate.Name,
				Description = entityToUpdate.Description,
				Likes = entityToUpdate.Likes,
				Muscles = entityToUpdate.Muscles.Select(m => new MuscleDto
				{
					Id = m.Id,
					Name = m.Name,
					Exercises = new List<ExerciseDto>(),
				}).ToList(),
				Workouts = entityToUpdate.Workouts.Select(w => new WorkoutDto
				{
					Id = w.Id,
					Name = w.Name,
					Description = w.Description,
					Exercises = new List<ExerciseDto>(),
					WorkoutPlans = new List<WorkoutPlanDto>(),
				}).ToList(),
			};
		}
	}
}
