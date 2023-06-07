using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
    public interface IExerciseValidators
    {
        public Task Validate(AddExerciseCommand command);
        public Task Validate(DeleteExerciseCommand command);
        public Task Validate(UpdateExerciseCommand command);
        public Task Validate(GetAllExerciseQuery query);
        public Task Validate(GetByIdExerciseQuery query);
    }
}
