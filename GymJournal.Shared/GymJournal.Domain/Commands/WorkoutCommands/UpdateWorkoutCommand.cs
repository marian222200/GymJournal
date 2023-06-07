using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.WorkoutCommands
{
	public class UpdateWorkoutCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid WorkoutId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public ICollection<Guid>? ExerciseIds { get; set; }
		public ICollection<Guid>? WorkoutPlanIds { get; set; }
	}
}
