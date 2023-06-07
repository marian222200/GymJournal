using GymJournal.Data.Context.IContext;
using GymJournal.Domain.Queries.MuscleQueries;
using GymJournal.Data.RequestValidators.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GymJournal.Data.RequestValidators.Validators
{
    public class MuscleValidators : IMuscleValidators
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IValidationAuthorization _validationAuthorization;

        public MuscleValidators(IApplicationDbContext dbContext, IValidationAuthorization validationAuthorization)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_validationAuthorization = validationAuthorization ?? throw new ArgumentNullException(nameof(validationAuthorization));
		}

        public async Task Validate(GetAllMuscleQuery query)
        {
            if (query == null)
            {
                throw new Exception("null query");
            }

            await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}

        public async Task Validate(GetByIdMuscleQuery query)
		{
			if (query == null)
			{
				throw new Exception("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}
    }
}
