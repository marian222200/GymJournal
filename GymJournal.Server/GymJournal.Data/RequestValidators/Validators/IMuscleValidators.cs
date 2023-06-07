using GymJournal.Domain.Queries.MuscleQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
    public interface IMuscleValidators
    {
        public Task Validate(GetAllMuscleQuery query);
        public Task Validate(GetByIdMuscleQuery query);
    }
}
