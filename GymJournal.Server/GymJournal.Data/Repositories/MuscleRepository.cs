using GymJournal.Data.Context.IContext;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.MuscleQueries;
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

		public async Task<GetAllMuscleResponse> GetAll(GetAllMuscleQuery query, CancellationToken cancellationToken = default)
		{
			var muscles = await _dbContext.Muscles
				.AsNoTracking()
				.Select(entity => new MuscleDto
				{
					Id = entity.Id,
					Name = entity.Name,
					Exercises = entity.Exercises.Select(e => new ExerciseDto
					{
						Id = e.Id,
						Name = e.Name,
						Description = e.Description,
						Likes = e.Likes,
						Muscles = new List<MuscleDto>(),
						Workouts = new List<WorkoutDto>(),
					}).ToList(),
				})
				.ToListAsync(cancellationToken);

			return new GetAllMuscleResponse { Muscles = muscles };
		}

		public async Task<GetByIdMuscleResponse> GetById(GetByIdMuscleQuery query, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Muscles
				.Include(e => e.Exercises)
				.FirstOrDefaultAsync(m => m.Id == query.MuscleId, cancellationToken);

			if (entity == null)
			{
				throw new Exception("The muscle you want to GetById does not exist.");
			}

			return new GetByIdMuscleResponse
			{
				Id = entity.Id,
				Name = entity.Name,
				Exercises = entity.Exercises.Select(e => new ExerciseDto
				{
					Id = e.Id,
					Name = e.Name,
					Description = e.Description,
					Likes = e.Likes,
					Muscles = new List<MuscleDto>(),
					Workouts = new List<WorkoutDto>(),
				}).ToList(),
			};
		}
	}
}
