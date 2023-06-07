using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymJournal.Domain.DTOs;

namespace GymJournal.Domain.Queries.ExerciseQueries
{
    public class GetAllExerciseResponse
    {
        public List<ExerciseDto> Exercises { get; set; }
	}
}
