using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkSetCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.WorkSetQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
	public interface IWorkSetValidators
	{
		public Task Validate(AddWorkSetCommand command);
		public Task Validate(DeleteWorkSetCommand command);
		public Task Validate(GetAllWorkSetQuery query);
		public Task Validate(GetByIdWorkSetQuery query);
	}
}
