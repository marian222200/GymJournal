using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Entities
{
    public class Workout
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; }
    }
}
