using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public class ExerciseRepository : IRepository<ExerciseDto>
	{
		private readonly IApplicationDbContext _dbContext;

		public ExerciseRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<ExerciseDto> Add(ExerciseDto dto, CancellationToken cancellationToken = default)
		{
			var entity = new Exercise
			{
				Id = Guid.NewGuid(),
				Name = dto.Name,
				Description = dto.Description,
				Muscles = await _dbContext.Muscles.Where(m => dto.MuscleIds.Contains(m.Id)).ToListAsync(),
				Workouts = await _dbContext.Workouts.Where(w => dto.WorkoutIds.Contains(w.Id)).ToListAsync(),
			};

			await _dbContext.Exercises.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new ExerciseDto
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				MuscleIds = entity.Muscles.Select(m => m.Id).ToList(),
				WorkoutIds = entity.Workouts.Select(w => w.Id).ToList(),
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
					MuscleIds = entity.Muscles.Select(m => m.Id).ToList(),
					WorkoutIds = entity.Workouts.Select(w => w.Id).ToList(),
				})
				.ToListAsync(cancellationToken);
			
			return dtos;
		}

		public async Task<ExerciseDto?> GetById(Guid? guid, CancellationToken cancellationToken = default)
		{
			if (guid == null)
			{
				return null;
			}

			var entity = await _dbContext.Exercises
				.Include(e => e.Muscles)
				.Include(e => e.Workouts)
				.FirstOrDefaultAsync(e => e.Id == guid, cancellationToken);

			if (entity == null)
			{
				return null;
			}

			var x = new ExerciseDto
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				MuscleIds = entity.Muscles.Select(m => m.Id).ToList(),
				WorkoutIds = entity.Workouts.Select(w => w.Id).ToList(),
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

		public async Task<ExerciseDto> Update(ExerciseDto dto, CancellationToken cancellationToken = default)
		{
			var entity = new Exercise
			{
				Id = dto.Id,
				Name = dto.Name,
				Description = dto.Description,
				Muscles = await _dbContext.Muscles.Where(m => dto.MuscleIds.Contains(m.Id)).ToListAsync(),
				Workouts = await _dbContext.Workouts.Where(w => dto.WorkoutIds.Contains(w.Id)).ToListAsync(),
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
			entityToUpdate.Muscles = entity.Muscles;
			entityToUpdate.Workouts = entity.Workouts;

			await SaveChanges(cancellationToken);

			return new ExerciseDto
			{
				Id = entityToUpdate.Id,
				Name = entityToUpdate.Name,
				Description = entityToUpdate.Description,
				MuscleIds = entityToUpdate.Muscles.Select(m => m.Id).ToList(),
				WorkoutIds = entityToUpdate.Workouts.Select(w => w.Id).ToList(),
			};
		}
	}
}
