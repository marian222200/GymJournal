using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.WorkoutCommands
{
	public class AddWorkoutCommand
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<Guid>? ExerciseIds { get; set; }
		public ICollection<Guid>? WorkoutPlanIds { get; set; }
	}
}
