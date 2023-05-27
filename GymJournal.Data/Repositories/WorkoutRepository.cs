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
	public class WorkoutRepository : IRepository<WorkoutDto>
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public WorkoutRepository(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public async Task<WorkoutDto> Add(WorkoutDto dto, CancellationToken cancellationToken = default)
		{
			var entity = _mapper.Map<Workout>(dto);
			await _dbContext.Workouts.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return _mapper.Map<WorkoutDto>(entity);
		}

		public async Task<IEnumerable<WorkoutDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var dtos = await _dbContext.Workouts
				.Include(w => w.Exercises)
					.ThenInclude(e => e.Muscles)
				.Include(w => w.WorkoutPlans)
				.AsNoTracking()
				.Select(w => _mapper.Map<WorkoutDto>(w))
				.ToListAsync(cancellationToken);

			return dtos;
		}

		public async Task<WorkoutDto?> GetById(Guid? guid, CancellationToken cancellationToken = default)
		{
			if (guid == null)
			{
				return null;
			}

			var entity = await _dbContext.Workouts
				.Include(w => w.Exercises)
					.ThenInclude(e => e.Muscles)
				.Include(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(w => w.Id == guid, cancellationToken);

			if (entity == null)
			{
				return null;
			}

			return _mapper.Map<WorkoutDto>(entity);
		}

		public async Task Remove(Guid? id, CancellationToken cancellationToken = default)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			var entity = await _dbContext.Workouts.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

			if (entity == null)
			{
				throw new ArgumentException("The workout you want to delete does not exist.");
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
			var entity = _mapper.Map<Workout>(dto);

			var entityToUpdate = await _dbContext.Workouts
				.FirstOrDefaultAsync(w => w.Id == entity.Id, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new ArgumentException("The workout you want to update does not exist.");
			}

			entityToUpdate.Name = entity.Name;
			entityToUpdate.Description = entity.Description;

			await SaveChanges(cancellationToken);
			return _mapper.Map<WorkoutDto>(entityToUpdate);
		}
	}
}
