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
    public class WorkoutRepository : IRepository<WorkoutDto>
	{
		private readonly IApplicationDbContext _dbContext;

		public WorkoutRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}
		public async Task<WorkoutDto> Add(WorkoutDto dto, CancellationToken cancellationToken = default)
		{
			var entity = new Workout
			{
				//Id = Guid.NewGuid(),
				Name = dto.Name,
				Description = dto.Description,
				Exercises = await _dbContext.Exercises.Where(e => dto.ExerciseIds.Contains(e.Id)).ToListAsync(),
				WorkoutPlans = await _dbContext.WorkoutPlans.Where(w => dto.WorkoutPlanIds.Contains(w.Id)).ToListAsync(),
			};

			await _dbContext.Workouts.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new WorkoutDto
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				ExerciseIds = entity.Exercises.Select(e => e.Id).ToList(),
				WorkoutPlanIds = entity.WorkoutPlans.Select(w => w.Id).ToList(),
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
					ExerciseIds = entity.Exercises.Select(e => e.Id).ToList(),
					WorkoutPlanIds = entity.WorkoutPlans.Select(w => w.Id).ToList(),
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
				ExerciseIds = entity.Exercises.Select(e => e.Id).ToList(),
				WorkoutPlanIds = entity.WorkoutPlans.Select(w => w.Id).ToList(),
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

		public async Task<WorkoutDto> Update(WorkoutDto dto, CancellationToken cancellationToken = default)
		{
			var entity = new Workout
			{
				Id = dto.Id,
				Name = dto.Name,
				Description = dto.Description,
				Exercises = await _dbContext.Exercises.Where(e => dto.ExerciseIds.Contains(e.Id)).ToListAsync(),
				WorkoutPlans = await _dbContext.WorkoutPlans.Where(w => dto.WorkoutPlanIds.Contains(w.Id)).ToListAsync(),
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
				ExerciseIds = entityToUpdate.Exercises.Select(e => e.Id).ToList(),
				WorkoutPlanIds = entityToUpdate.WorkoutPlans.Select(w => w.Id).ToList(),
			};
		}
	}
}
