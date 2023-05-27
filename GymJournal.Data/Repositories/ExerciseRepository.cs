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
	public class ExerciseRepository : IRepository<ExerciseDto>
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public ExerciseRepository(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<ExerciseDto> Add(ExerciseDto dto, CancellationToken cancellationToken = default)
		{
			var entity = _mapper.Map<Exercise>(dto);
			await _dbContext.Exercises.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);
			
			return _mapper.Map<ExerciseDto>(entity);
		}

		public async Task<IEnumerable<ExerciseDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var dtos = await _dbContext.Exercises
				.Include(e => e.Muscles)
				.Include(e => e.Workouts)
					.ThenInclude(w => w.WorkoutPlans)
				.AsNoTracking()
				.Select(e => _mapper.Map<ExerciseDto>(e))
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
				.Include (e => e.Muscles)
				.Include(e => e.Workouts)
					.ThenInclude(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(e => e.Id == guid, cancellationToken);

			if (entity == null)
			{
				return null;
			}

			return _mapper.Map<ExerciseDto>(entity);
		}

		public async Task Remove(Guid? id, CancellationToken cancellationToken = default)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			var entity = await _dbContext.Exercises.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

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
			var entity = _mapper.Map<Exercise>(dto);

			var entityToUpdate = await _dbContext.Exercises
				.FirstOrDefaultAsync(e => e.Id== entity.Id, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new ArgumentException("The exercise you want to update does not exist.");
			}

			entityToUpdate.Name = entity.Name;
			entityToUpdate.Description = entity.Description;

			await SaveChanges(cancellationToken);
			return _mapper.Map<ExerciseDto>(entityToUpdate);
		}
	}
}
