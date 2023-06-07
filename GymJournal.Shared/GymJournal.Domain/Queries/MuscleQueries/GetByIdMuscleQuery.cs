using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Queries.MuscleQueries
{
	public class GetByIdMuscleQuery
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid MuscleId { get; set; }
	}
}
