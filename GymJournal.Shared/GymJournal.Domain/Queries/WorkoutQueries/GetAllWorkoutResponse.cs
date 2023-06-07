using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymJournal.Domain.DTOs;

namespace GymJournal.Domain.Queries.WorkoutQueries
{
    public class GetAllWorkoutResponse
    {
        public List<WorkoutDto> Workouts { get; set; }
    }
}
