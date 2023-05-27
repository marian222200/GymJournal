using AutoMapper;
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
	public class WorkoutPlanRepository : IRepository<WorkoutPlanDto>
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public WorkoutPlanRepository(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public async Task<WorkoutPlanDto> Add(WorkoutPlanDto dto, CancellationToken cancellationToken = default)
		{
			var entity = _mapper.Map<WorkoutPlan>(dto);
			await _dbContext.WorkoutPlans.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return _mapper.Map<WorkoutPlanDto>(entity);
		}

		public async Task<IEnumerable<WorkoutPlanDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var dtos = await _dbContext.WorkoutPlans
				.Include(w => w.Workouts)
					.ThenInclude(w => w.Exercises)
						.ThenInclude(e => e.Muscles)
				.AsNoTracking()
				.Select(w => _mapper.Map<WorkoutPlanDto>(w))
				.ToListAsync(cancellationToken);

			return dtos;
		}

		public async Task<WorkoutPlanDto?> GetById(Guid? guid, CancellationToken cancellationToken = default)
		{
			if (guid == null)
			{
				return null;
			}

			var entity = await _dbContext.WorkoutPlans
				.Include(w => w.Workouts)
					.ThenInclude(w => w.Exercises)
						.ThenInclude(e => e.Muscles)
				.FirstOrDefaultAsync(w => w.Id == guid, cancellationToken);

			if (entity == null)
			{
				return null;
			}

			return _mapper.Map<WorkoutPlanDto>(entity);
		}

		public async Task Remove(Guid? id, CancellationToken cancellationToken = default)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			var entity = await _dbContext.WorkoutPlans.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

			if (entity == null)
			{
				throw new ArgumentException("The workoutPlan you want to delete does not exist.");
			}

			_dbContext.WorkoutPlans.Remove(entity);
			await SaveChanges(cancellationToken);
		}

		public async Task SaveChanges(CancellationToken cancellationToken = default)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<WorkoutPlanDto> Update(WorkoutPlanDto dto, CancellationToken cancellationToken = default)
		{
			var entity = _mapper.Map<WorkoutPlan>(dto);

			var entityToUpdate = await _dbContext.WorkoutPlans
				.FirstOrDefaultAsync(w => w.Id == entity.Id, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new ArgumentException("The workoutPlan you want to update does not exist.");
			}

			entityToUpdate.Name = entity.Name;
			entityToUpdate.Description = entity.Description;

			await SaveChanges(cancellationToken);
			return _mapper.Map<WorkoutPlanDto>(entityToUpdate);
		}
	}
}
