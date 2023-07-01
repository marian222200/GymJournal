using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.WorkSetCommands
{
	public class AddWorkSetCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid ExerciseId { get; set; }
		public string Date { get; set; }
		public string Weight { get; set; }
		public string Reps { get; set; }
	}
}
