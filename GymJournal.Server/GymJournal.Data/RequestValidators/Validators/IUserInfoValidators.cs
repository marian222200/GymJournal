using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.UserInfoCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.UserInfoQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Validators
{
    public interface IUserInfoValidators
	{
		public Task Validate(AddUserInfoCommand command);
		public Task Validate(DeleteUserInfoCommand command);
		public Task Validate(UpdateUserInfoCommand command);
		public Task Validate(GetAllUserInfoQuery query);
		public Task Validate(GetByIdUserInfoQuery query);
		public Task Validate(LoginUserInfoQuery query);
	}
}
