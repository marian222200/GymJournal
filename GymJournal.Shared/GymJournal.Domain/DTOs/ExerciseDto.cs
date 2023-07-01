using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.DTOs
{
    public class ExerciseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MuscleDto> Muscles { get; set; }
        public ICollection<WorkoutDto> Workouts { get; set; }
        public ICollection<WorkSetDto> WorkSets { get; set; }
    }
}
