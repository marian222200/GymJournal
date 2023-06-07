using GymJournal.Data.Context.IContext;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Domain.Commands.UserInfoCommands;
using GymJournal.Domain.Queries.UserInfoQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
    public class UserInfoValidators : IUserInfoValidators
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly IValidationAuthorization _validationAuthorization;

		public UserInfoValidators(IApplicationDbContext dbContext, IValidationAuthorization validationAuthorization)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_validationAuthorization = validationAuthorization ?? throw new ArgumentNullException(nameof(validationAuthorization));
		}

		public async Task Validate(AddUserInfoCommand command)
		{
			if (command == null)
			{
				throw new InvalidRequestException("null command");
			}

			if (await _dbContext.UserInfos.AnyAsync(u => u.Name == command.Name))
			{
				throw new BadRequestException("name already used");
			}
		}

		public async Task Validate(DeleteUserInfoCommand command)
		{
			if (command == null)
			{
				throw new InvalidRequestException("null command");
			}

			if (command.DeleteId != command.UserId)
			{
				await _validationAuthorization.ValidateAdminUser(command.UserId, command.UserToken);
			}
			else
			{
				await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
			}
		}

		public async Task Validate(UpdateUserInfoCommand command)
		{
			if (command == null)
			{
				throw new InvalidRequestException("null command");
			}

			if (command.Name != null && await _dbContext.UserInfos.AnyAsync(u => u.Name == command.Name))
			{
				throw new BadRequestException("name already used");
			}

			if ((command.UpdateId != command.UserId) || (command.Role != null))
			{
				await _validationAuthorization.ValidateAdminUser(command.UserId, command.UserToken);
			}
			else
			{
				await _validationAuthorization.ValidateRegularUser(command.UserId, command.UserToken);
			}
		}

		public async Task Validate(GetAllUserInfoQuery query)
		{
			if (query == null)
			{
				throw new InvalidRequestException("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}

		public async Task Validate(GetByIdUserInfoQuery query)
		{
			if (query == null)
			{
				throw new InvalidRequestException("null query");
			}

			await _validationAuthorization.ValidateRegularUser(query.UserId, query.UserToken);
		}

		public Task Validate(LoginUserInfoQuery query)
		{
			return Task.CompletedTask;
		}
	}
}
