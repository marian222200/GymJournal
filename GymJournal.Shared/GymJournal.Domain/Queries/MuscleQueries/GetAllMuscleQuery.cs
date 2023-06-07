using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Queries.MuscleQueries
{
	public class GetAllMuscleQuery
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
	}
}
