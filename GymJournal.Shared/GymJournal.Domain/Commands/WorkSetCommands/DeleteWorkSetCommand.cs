using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.WorkSetCommands
{
	public class DeleteWorkSetCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid WorkSetId { get; set; }
	}
}
