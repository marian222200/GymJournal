using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymJournal.Domain.DTOs;

namespace GymJournal.Domain.Queries.MuscleQueries
{
    public class GetAllMuscleResponse
    {
        public List<MuscleDto> Muscles { get; set; }
	}
}
