using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.ExerciseCommands
{
    public class AddExerciseCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public ICollection<Guid> MuscleIds { get; set; }
        public ICollection<Guid> WorkoutIds { get; set; }
    }
}
