using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Entities.WorkoutTypes
{
	public class PastWorkout
	{
		public Guid Id { get; set; }
		public int? Score { get; set; }
		public string Date { get; set; }
		public string? Review { get; set; }
		public Guid WorkoutId { get; set; }
		public Workout Workout { get; set; }
	}
}
