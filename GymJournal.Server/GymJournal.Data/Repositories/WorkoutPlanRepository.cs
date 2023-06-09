using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.WorkoutPlanQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
    public class WorkoutPlanRepository : IWorkoutPlanRepository
	{
		private readonly IApplicationDbContext _dbContext;

		public WorkoutPlanRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<AddWorkoutPlanResponse> Add(AddWorkoutPlanCommand command, CancellationToken cancellationToken = default)
		{
			var entity = new WorkoutPlan
			{
				Name = command.Name,
				Description = command.Description,
				Workouts = await _dbContext.Workouts.Where(w => command.WorkoutIds.Contains(w.Id)).ToListAsync(),
			};

			await _dbContext.WorkoutPlans.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new AddWorkoutPlanResponse
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
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

		public async Task Delete(DeleteWorkoutPlanCommand command, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.WorkoutPlans
				.Include(w => w.Workouts)
				.FirstOrDefaultAsync(w => w.Id == command.WorkoutPlanId, cancellationToken);

			if (entity == null)
			{
				throw new BadRequestException("The WorkoutPlan you want to delete does not exist.");
			}

			_dbContext.WorkoutPlans.Remove(entity);
			await SaveChanges(cancellationToken);
		}

		public async Task<GetAllWorkoutPlanResponse> GetAll(GetAllWorkoutPlanQuery query, CancellationToken cancellationToken = default)
		{
			var workoutPlans = await _dbContext.WorkoutPlans
				.AsNoTracking()
				.Select(entity => new WorkoutPlanDto
				{
					Id = entity.Id,
					Name = entity.Name,
					Description = entity.Description,
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

			return new GetAllWorkoutPlanResponse { WorkoutPlans = workoutPlans };
		}

		public async Task<GetByIdWorkoutPlanResponse> GetById(GetByIdWorkoutPlanQuery query, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.WorkoutPlans
				.Include(w => w.Workouts)
				.FirstOrDefaultAsync(w => w.Id == query.WorkoutPlanId, cancellationToken);

			if (entity == null)
			{
				throw new BadRequestException("The WorkoutPlan you want to GetById does not exist.");
			}

			return new GetByIdWorkoutPlanResponse
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
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

		public async Task SaveChanges(CancellationToken cancellationToken = default)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<UpdateWorkoutPlanResponse> Update(UpdateWorkoutPlanCommand command, CancellationToken cancellationToken = default)
		{
			var entityToUpdate = await _dbContext.WorkoutPlans
				.Include(w => w.Workouts)
				.FirstOrDefaultAsync(w => w.Id == command.WorkoutPlanId, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new BadRequestException("The workoutPlan you want to update does not exist.");
			}

			if (command.Name != null) { entityToUpdate.Name = command.Name; }
			if (command.Description != null) { entityToUpdate.Description = command.Description; }
			if (command.WorkoutIds != null)
			{
				entityToUpdate.Workouts =
					await _dbContext.Workouts.Where(w => command.WorkoutIds.Contains(w.Id)).ToListAsync();
			}

			await SaveChanges(cancellationToken);

			return new UpdateWorkoutPlanResponse
			{
				Id = entityToUpdate.Id,
				Name = entityToUpdate.Name,
				Description = entityToUpdate.Description,
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
