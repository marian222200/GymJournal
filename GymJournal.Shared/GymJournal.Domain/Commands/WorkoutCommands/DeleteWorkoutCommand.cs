using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.WorkoutCommands
{
	public class DeleteWorkoutCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid WorkoutId { get; set; }
	}
}
