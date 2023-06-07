using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Queries.UserInfoQueries
{
	public class GetByIdUserInfoQuery
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid GetId { get; set; }
	}
}
