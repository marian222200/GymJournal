using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.WorkoutPlanQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
    public interface IWorkoutPlanValidators
	{
		public Task Validate(AddWorkoutPlanCommand command);
		public Task Validate(DeleteWorkoutPlanCommand command);
		public Task Validate(UpdateWorkoutPlanCommand command);
		public Task Validate(GetAllWorkoutPlanQuery query);
		public Task Validate(GetByIdWorkoutPlanQuery query);
	}
}
