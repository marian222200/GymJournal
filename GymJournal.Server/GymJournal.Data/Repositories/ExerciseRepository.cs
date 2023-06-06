using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.DTOs;
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

		public async Task<ExerciseDto> Add(AddExerciseCommand command, CancellationToken cancellationToken = default)
		{
			var entity = new Exercise
			{
				//Id = Guid.NewGuid(),
				Name = command.Name,
				Description = command.Description,
				Likes = command.Likes,
				Muscles = await _dbContext.Muscles.Where(m => command.MuscleIds.Contains(m.Id)).ToListAsync(),
				Workouts = await _dbContext.Workouts.Where(w => command.WorkoutIds.Contains(w.Id)).ToListAsync(),
			};

			await _dbContext.Exercises.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new ExerciseDto
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

		public async Task<IEnumerable<ExerciseDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var dtos = await _dbContext.Exercises
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
			
			return dtos;
		}

		public async Task<ExerciseDto?> GetById(Guid? guid, CancellationToken cancellationToken = default)
		{
			if (guid == null)
			{
				throw new ArgumentNullException(nameof(guid));
			}

			var entity = await _dbContext.Exercises
				.Include(e => e.Muscles)
				.Include(e => e.Workouts)
				.FirstOrDefaultAsync(e => e.Id == guid, cancellationToken);

			if (entity == null)
			{
				throw new ArgumentException("The exercise you want to GetById does not exist.");
			}

			var x = new ExerciseDto
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

		public async Task Remove(Guid? id, CancellationToken cancellationToken = default)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			var entity = await _dbContext.Exercises
				.Include(e => e.Muscles)
				.Include(e => e.Workouts)
				.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

			if (entity == null)
			{
				throw new ArgumentException("The exercise you want to delete does not exist.");
			}

			_dbContext.Exercises.Remove(entity);
			await SaveChanges(cancellationToken);
		}

		public async Task SaveChanges(CancellationToken cancellationToken = default)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<ExerciseDto> Update(UpdateExerciseCommand command, CancellationToken cancellationToken = default)
		{
			var entity = new Exercise
			{
				Id = command.Id,
				Name = command.Name,
				Description = command.Description,
				Likes = command.Likes,
				Muscles = await _dbContext.Muscles.Where(m => command.MuscleIds.Contains(m.Id)).ToListAsync(),
				Workouts = await _dbContext.Workouts.Where(w => command.WorkoutIds.Contains(w.Id)).ToListAsync(),
			};

			var entityToUpdate = await _dbContext.Exercises
				.Include(e => e.Muscles)
				.Include(e => e.Workouts)
				.FirstOrDefaultAsync(e => e.Id== entity.Id, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new ArgumentException("The exercise you want to update does not exist.");
			}

			entityToUpdate.Name = entity.Name;
			entityToUpdate.Description = entity.Description;
			entityToUpdate.Likes = entity.Likes;
			entityToUpdate.Muscles = entity.Muscles;
			entityToUpdate.Workouts = entity.Workouts;

			await SaveChanges(cancellationToken);

			return new ExerciseDto
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
