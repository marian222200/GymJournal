using GymJournal.Data.Context.IContext;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Domain.Commands.WorkSetCommands;
using GymJournal.Domain.Queries.WorkSetQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
	public class WorkSetValidators : IWorkSetValidators
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IValidationAuthorization _validationAuthorization;

		public WorkSetValidators(IApplicationDbContext dbContext, IValidationAuthorization validationAuthorization)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_validationAuthorization = validationAuthorization ?? throw new ArgumentNullException(nameof(validationAuthorization));
		}

		public async Task Validate(AddWorkSetCommand command)
		{
			if (command == null)
			{
				throw new InvalidRequestException("null command");
			}
			if (!await _dbContext.Exercises.AnyAsync(e => e.Id == command.ExerciseId))
			{
				throw new BadRequestException("inexistent exercise");
			}
			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(DeleteWorkSetCommand command)
		{
			if (command == null)
			{
				throw new InvalidRequestException("null command");
			}

			var workSet = _dbContext.WorkSets.FirstOrDefault(e => e.Id == command.WorkSetId);

			if (workSet == null)
			{
				throw new BadRequestException("invalid ExerciseId");
			}

			if (workSet.UserId != command.UserId)
			{
				throw new UnauthorizedAccessException("unauthorized User");
			}

			await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
		}

		public async Task Validate(GetAllWorkSetQuery query)
		{
			if (query == null)
			{
				throw new InvalidRequestException("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}

		public async Task Validate(GetByIdWorkSetQuery query)
		{
			if (query == null)
			{
				throw new InvalidRequestException("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}
	}
}
