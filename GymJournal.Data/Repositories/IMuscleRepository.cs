using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public interface IMuscleRepository
	{
		Task<IEnumerable<MuscleDto>> GetAll(CancellationToken cancellationToken = default);
		Task<MuscleDto?> GetById(Guid? guid, CancellationToken cancellationToken = default);
	}
}
