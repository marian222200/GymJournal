using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.ExerciseCommands
{
	public class DeleteExerciseCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid ExerciseId { get; set;}
	}
}
