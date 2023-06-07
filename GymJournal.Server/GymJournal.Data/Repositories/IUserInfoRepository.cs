using GymJournal.Domain.Commands.UserInfoCommands;
using GymJournal.Domain.Queries.UserInfoQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public interface IUserInfoRepository
	{
		public Task<AddUserInfoResponse> Add(AddUserInfoCommand command, CancellationToken cancellationToken = default);
		public Task<UpdateUserInfoResponse> Update(UpdateUserInfoCommand command, CancellationToken cancellationToken = default);
		public Task Delete(DeleteUserInfoCommand command, CancellationToken cancellationToken = default);
		public Task<GetAllUserInfoResponse> GetAll(GetAllUserInfoQuery query, CancellationToken cancellationToken = default);
		public Task<GetByIdUserInfoResponse> GetById(GetByIdUserInfoQuery query, CancellationToken cancellationToken = default);
		public Task<LoginUserInfoResponse> Login(LoginUserInfoQuery query, CancellationToken cancellationToken = default);
		Task SaveChanges(CancellationToken cancellationToken = default);
	}
}
