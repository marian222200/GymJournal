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

		public MuscleRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}
		public async Task<IEnumerable<MuscleDto>> GetAll(CancellationToken cancellationToken = default)
		{
			var dtos = await _dbContext.Muscles
				.AsNoTracking()
				.Select(entity => new MuscleDto
				{
					Id = entity.Id,
					Name = entity.Name,
					ExerciseIds = entity.Exercises.Select(e => e.Id).ToList(),
				})
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
				.Include(e => e.Exercises)
				.FirstOrDefaultAsync(m => m.Id == guid, cancellationToken);

			if (entity == null)
			{
				return null;
			}

			return new MuscleDto
			{
				Id = entity.Id,
				Name = entity.Name,
				ExerciseIds = entity.Exercises.Select(e => e.Id).ToList(),
			};
		}
	}
}
