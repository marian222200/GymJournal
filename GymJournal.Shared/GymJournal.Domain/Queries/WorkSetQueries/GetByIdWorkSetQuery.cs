using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Queries.WorkSetQueries
{
	public class GetByIdWorkSetQuery
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid WorkSetId { get; set; }
	}
}
