using GymJournal.Data.Entities.WorkoutTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Entities.ExerciseTypes
{
	public class ExerciseEntry
	{
		public Guid Id { get; set; }
		public int MinRepRange { get; set; }
		public int MaxRepRange { get; set; }
		public Guid ExerciseId { get; set; }
		public Exercise Exercise { get; set; }
		public ICollection<Workout> Workouts { get; set; }
	}
}
