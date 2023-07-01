using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.DTOs
{
	public class WorkSetDto
	{
		public Guid Id { get; set; }
		public string Date { get; set; }
		public string Weight { get; set; }
		public string Reps { get; set; }
		public Guid ExerciseId { get; set; }
		public Guid UserId { get; set; }
	}
}
