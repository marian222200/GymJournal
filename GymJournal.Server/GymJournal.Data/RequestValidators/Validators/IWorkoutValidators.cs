using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.WorkoutQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
    public interface IWorkoutValidators
	{
		public Task Validate(AddWorkoutCommand command);
		public Task Validate(DeleteWorkoutCommand command);
		public Task Validate(UpdateWorkoutCommand command);
		public Task Validate(GetAllWorkoutQuery query);
		public Task Validate(GetByIdWorkoutQuery query);
	}
}
