using GymJournal.Data.Context.IContext;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GymJournal.Data.RequestValidators.Validators
{
    public class ExerciseValidators : IExerciseValidators
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IValidationAuthorization _validationAuthorization;

		public ExerciseValidators(IApplicationDbContext dbContext, IValidationAuthorization validationAuthorization)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_validationAuthorization = validationAuthorization ?? throw new ArgumentNullException(nameof(validationAuthorization));
		}

		public async Task Validate(AddExerciseCommand command)
		{
			if (command == null)
			{
				throw new InvalidRequestException("null command");
			}

			foreach (var muscleId in command.MuscleIds)
			{
				if (!await _dbContext.Muscles.AnyAsync(m => m.Id == muscleId))
				{
					throw new BadRequestException("muscle list has inexistent muscles");
				}
			}

			foreach (var workoutId in command.WorkoutIds)
			{
				if (!await _dbContext.Workouts.AnyAsync(w => w.Id == workoutId))
				{
					throw new BadRequestException("workout list has inexistent workouts");
				}
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(DeleteExerciseCommand command)
		{
			if (command == null)
			{
				throw new InvalidRequestException("null command");
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(UpdateExerciseCommand command)
		{
			if (command == null)
			{
				throw new InvalidRequestException("null command");
			}

			if (command.MuscleIds != null)
			{
				foreach (var muscleId in command.MuscleIds)
				{
					if (!await _dbContext.Muscles.AnyAsync(m => m.Id == muscleId))
					{
						throw new BadRequestException("muscle list has inexistent muscles");
					}
				}
			}

			if (command.WorkoutIds != null)
			{
				foreach (var workoutId in command.WorkoutIds)
				{
					if (!await _dbContext.Workouts.AnyAsync(w => w.Id == workoutId))
					{
						throw new BadRequestException("workout list has inexistent workouts");
					}
				}
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(GetAllExerciseQuery query)
		{
			if (query == null)
			{
				throw new InvalidRequestException("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}

		public async Task Validate(GetByIdExerciseQuery query)
		{
			if (query == null)
			{
				throw new InvalidRequestException("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}
	}
}
