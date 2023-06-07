using GymJournal.Domain.Queries.MuscleQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
    public interface IMuscleRepository
	{
		public Task<GetAllMuscleResponse> GetAll(GetAllMuscleQuery query, CancellationToken cancellationToken = default);
		public Task<GetByIdMuscleResponse> GetById(GetByIdMuscleQuery query, CancellationToken cancellationToken = default);
	}
}
