using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Entities
{
	public class WorkSet
	{
		public Guid Id { get; set; }
		public string Date { get; set; }
		public string Weight { get; set; }
		public string Reps { get; set; }
		public Guid ExerciseId { get; set; }
		public Exercise Exercise { get; set; }
		public Guid UserId { get; set; }
		public UserInfo User { get; set; }
	}
}
