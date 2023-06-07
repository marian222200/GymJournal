using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services
{
	public class IdentityService
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
	}
}
