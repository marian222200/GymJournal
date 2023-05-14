using GymJournal.Data.Entities.WorkoutTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Entities
{
	public class WorkoutPlan
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Likes { get; set; }
		public bool IsPrivate { get; set; }
		public ICollection<Workout> Workouts { get; set; }
		//Coach that added the exercise
	}
}
