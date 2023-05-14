using GymJournal.Data.Entities.WorkoutTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Entities.ExerciseTypes
{
	public class PastExercise
	{
		public Guid Id { get; set; }
		public int Reps { get; set; }
		public int Weight { get; set; }
		public int? Score { get; set; }
		public string? Review { get; set; }
		public Guid ExerciseId { get; set; }
		public Exercise Exercise { get; set; }
		public Guid WorkoutId { get; set; }
		public Workout Workout { get; set; }
	}
}
