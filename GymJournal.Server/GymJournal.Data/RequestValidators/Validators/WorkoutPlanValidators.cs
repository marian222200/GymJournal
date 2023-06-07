using GymJournal.Data.Context.IContext;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.Queries.WorkoutPlanQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
    public class WorkoutPlanValidators : IWorkoutPlanValidators
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IValidationAuthorization _validationAuthorization;

		public WorkoutPlanValidators(IApplicationDbContext dbContext, IValidationAuthorization validationAuthorization)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_validationAuthorization = validationAuthorization ?? throw new ArgumentNullException(nameof(validationAuthorization));
		}

		public async Task Validate(AddWorkoutPlanCommand command)
		{
			if (command == null)
			{
				throw new Exception("null command");
			}

			foreach (var workoutId in command.WorkoutIds)
			{
				if (!await _dbContext.Workouts.AnyAsync(w => w.Id == workoutId))
				{
					throw new Exception("workout list has inexistent workouts");
				}
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(DeleteWorkoutPlanCommand command)
		{
			if (command == null)
			{
				throw new Exception("null command");
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(UpdateWorkoutPlanCommand command)
		{
			if (command == null)
			{
				throw new Exception("null command");
			}

			if (command.WorkoutIds != null)
			{
				foreach (var workoutId in command.WorkoutIds)
				{
					if (!await _dbContext.Workouts.AnyAsync(w => w.Id == workoutId))
					{
						throw new Exception("workout list has inexistent workouts");
					}
				}
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(GetAllWorkoutPlanQuery query)
		{
			if (query == null)
			{
				throw new Exception("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}

		public async Task Validate(GetByIdWorkoutPlanQuery query)
		{
			if (query == null)
			{
				throw new Exception("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}
	}
}
