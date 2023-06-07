using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.WorkoutPlanCommands
{
	public class DeleteWorkoutPlanCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid WorkoutPlanId { get; set; }
	}
}
