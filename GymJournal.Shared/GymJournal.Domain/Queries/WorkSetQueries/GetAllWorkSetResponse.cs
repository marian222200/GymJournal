using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Queries.WorkSetQueries
{
	public class GetAllWorkSetResponse
	{
		public List<WorkSetDto> WorkSets { get; set; }
	}
}
