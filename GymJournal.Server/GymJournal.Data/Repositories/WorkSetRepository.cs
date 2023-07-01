using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkSetCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.WorkSetQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public class WorkSetRepository : IWorkSetRepository
	{
		private readonly IApplicationDbContext _dbContext;

		public WorkSetRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<AddWorkSetResponse> Add(AddWorkSetCommand command, CancellationToken cancellationToken = default)
		{
			var commandExercise = await _dbContext.Exercises.FirstOrDefaultAsync(e => e.Id == command.ExerciseId);
			var entity = new WorkSet
			{
				Date = command.Date,
				Weight = command.Weight,
				Reps = command.Reps,
				ExerciseId = command.ExerciseId,
				Exercise = await _dbContext.Exercises.FirstOrDefaultAsync(e => e.Id == command.ExerciseId),
				UserId = command.UserId,
				User = await _dbContext.UserInfos.FirstOrDefaultAsync(e => e.Id == command.ExerciseId),
			};

			await _dbContext.WorkSets.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new AddWorkSetResponse
			{
				Id = entity.Id,
				Date = entity.Date,
				Weight = entity.Weight,
				Reps = entity.Reps,
				ExerciseId = entity.ExerciseId,
				UserId = entity.UserId,
			};
		}

		public async Task Delete(DeleteWorkSetCommand command, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.WorkSets
				.FirstOrDefaultAsync(e => e.Id == command.WorkSetId, cancellationToken);

			if (entity == null)
			{
				throw new BadRequestException("The workSet you want to delete does not exist.");
			}

			_dbContext.WorkSets.Remove(entity);
			await SaveChanges(cancellationToken);
		}

		public async Task<GetAllWorkSetResponse> GetAll(GetAllWorkSetQuery query, CancellationToken cancellationToken = default)
		{
			var workSets = await _dbContext.WorkSets
				.AsNoTracking()
				.Select(entity => new WorkSetDto
				{
					Id = entity.Id,
					Date = entity.Date,
					Weight = entity.Weight,
					Reps = entity.Reps,
					ExerciseId = entity.ExerciseId,
					UserId = entity.UserId,
				})
				.ToListAsync(cancellationToken);

			return new GetAllWorkSetResponse { WorkSets = workSets };
		}

		public async Task<GetByIdWorkSetResponse> GetById(GetByIdWorkSetQuery query, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.WorkSets
				.FirstOrDefaultAsync(w => w.Id == query.WorkSetId, cancellationToken);

			if (entity == null)
			{
				throw new BadRequestException("The workSet you want to GetById does not exist.");
			}

			var x = new GetByIdWorkSetResponse
			{
				Id = entity.Id,
				Date = entity.Date,
				Weight = entity.Weight,
				Reps = entity.Reps,
				ExerciseId = entity.ExerciseId,
				UserId = entity.UserId,
			};

			return x;
		}

		public async Task SaveChanges(CancellationToken cancellationToken = default)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
