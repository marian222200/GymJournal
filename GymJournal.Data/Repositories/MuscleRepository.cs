using AutoMapper;
using GymJournal.Data.Context.IContext;
using GymJournal.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public class MuscleRepository : IMuscleRepository
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IMapper _mapper;

		public MuscleRepository(IApplicationDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public async Task<IEnumerable<MuscleDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var dtos = await _dbContext.Muscles
				.Include(m => m.Exercises)
					.ThenInclude(e => e.Workouts)
						.ThenInclude(w => w.WorkoutPlans)
				.AsNoTracking()
				.Select(m => _mapper.Map<MuscleDto>(m))
				.ToListAsync(cancellationToken);

			return dtos;
		}

		public async Task<MuscleDto?> GetById(Guid? guid, CancellationToken cancellationToken = default)
		{
			if (guid == null)
			{
				return null;
			}

			var entity = await _dbContext.Muscles
				.Include(m => m.Exercises)
					.ThenInclude(e => e.Workouts)
						.ThenInclude(w => w.WorkoutPlans)
				.FirstOrDefaultAsync(m => m.Id == guid, cancellationToken);

			if (entity == null)
			{
				return null;
			}

			return _mapper.Map<MuscleDto>(entity);
		}
	}
}
