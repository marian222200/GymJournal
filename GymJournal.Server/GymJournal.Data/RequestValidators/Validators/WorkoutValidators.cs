using GymJournal.Data.Context.IContext;
using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.Queries.WorkoutQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
	public class WorkoutValidators : IWorkoutValidators
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IValidationAuthorization _validationAuthorization;

		public WorkoutValidators(IApplicationDbContext dbContext, IValidationAuthorization validationAuthorization)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_validationAuthorization = validationAuthorization ?? throw new ArgumentNullException(nameof(validationAuthorization));
		}

		public async Task Validate(AddWorkoutCommand command)
		{

			if (command == null)
			{
				throw new Exception("null command");
			}

			foreach (var exerciseId in command.ExerciseIds)
			{
				if (!await _dbContext.Exercises.AnyAsync(e => e.Id == exerciseId))
				{
					throw new Exception("exercise list has inexistent exercises");
				}
			}

			foreach (var workoutPlanId in command.WorkoutPlanIds)
			{
				if (!await _dbContext.WorkoutPlans.AnyAsync(w => w.Id == workoutPlanId))
				{
					throw new Exception("workoutPlan list has inexistent workoutPlans");
				}
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(DeleteWorkoutCommand command)
		{
			if (command == null)
			{
				throw new Exception("null command");
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(UpdateWorkoutCommand command)
		{
			if (command == null)
			{
				throw new Exception("null command");
			}

			if (command.ExerciseIds != null)
			{
				foreach (var exerciseId in command.ExerciseIds)
				{
					if (!await _dbContext.Exercises.AnyAsync(e => e.Id == exerciseId))
					{
						throw new Exception("exercise list has inexistent exercises");
					}
				}
			}

			if (command.WorkoutPlanIds != null)
			{
				foreach (var workoutPlanId in command.WorkoutPlanIds)
				{
					if (!await _dbContext.WorkoutPlans.AnyAsync(w => w.Id == workoutPlanId))
					{
						throw new Exception("workoutPlan list has inexistent workoutPlans");
					}
				}
			}
			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(GetAllWorkoutQuery query)
		{
			if (query == null)
			{
				throw new Exception("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}

		public async Task Validate(GetByIdWorkoutQuery query)
		{
			if (query == null)
			{
				throw new Exception("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}
	}
}
