using BCrypt.Net;
using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.UserInfoCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.UserInfoQueries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace GymJournal.Data.Repositories
{
	public class UserInfoRepository : IUserInfoRepository
	{
		private readonly IApplicationDbContext _dbContext;

		public UserInfoRepository(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public async Task<AddUserInfoResponse> Add(AddUserInfoCommand command, CancellationToken cancellationToken = default)
		{
			var entity = new UserInfo
			{
				Name = command.Name,
				Password = BCryptNet.HashPassword(command.Password),
				Role = "Regular",
				Token = Guid.NewGuid(),
			};

			await _dbContext.UserInfos.AddAsync(entity, cancellationToken);
			await SaveChanges(cancellationToken);

			return new AddUserInfoResponse
			{
				UserId = entity.Id,
				UserToken = entity.Token,
				UserName = entity.Name,
				UserRole = entity.Role,
			};
		}

		public async Task Delete(DeleteUserInfoCommand command, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.UserInfos
				.FirstOrDefaultAsync(e => e.Id == command.DeleteId, cancellationToken);

			if (entity == null)
			{
				throw new BadRequestException("The userInfo you want to delete does not exist.");
			}

			_dbContext.UserInfos.Remove(entity);
			await SaveChanges(cancellationToken);
		}

		public async Task<GetAllUserInfoResponse> GetAll(GetAllUserInfoQuery query, CancellationToken cancellationToken = default)
		{
			var userInfos = await _dbContext.UserInfos
				.AsNoTracking()
				.Select(entity => new UserInfoDto
				{
					Id = entity.Id,
					Name = entity.Name,
					Role = entity.Role,
				})
				.ToListAsync(cancellationToken);

			return new GetAllUserInfoResponse { UserInfos = userInfos };
		}

		public async Task<GetByIdUserInfoResponse> GetById(GetByIdUserInfoQuery query, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.UserInfos
				.FirstOrDefaultAsync(e => e.Id == query.GetId, cancellationToken);

			if (entity == null)
			{
				throw new BadRequestException("The userInfo you want to GetById does not exist.");
			}

			var x = new GetByIdUserInfoResponse
			{
				Id = entity.Id,
				Name = entity.Name,
				Role = entity.Role,
			};

			return x;
		}

		public async Task<LoginUserInfoResponse> Login(LoginUserInfoQuery query, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.UserInfos
				.FirstOrDefaultAsync(e => e.Name == query.UserName, cancellationToken);

			if (entity == null)
			{
				throw new BadRequestException("The userInfo you want to Login does not exist.");
			}

			if (!BCryptNet.Verify(query.UserPassword, entity.Password))
			{
				throw new UnauthorizedAccessException("Invalid password for login.");
			}

			entity.Token = Guid.NewGuid();
			await SaveChanges(cancellationToken);

			return new LoginUserInfoResponse
			{
				UserId = entity.Id,
				UserToken = entity.Token,
				UserName = entity.Name,
				UserRole = entity.Role,
			};
		}

		public async Task SaveChanges(CancellationToken cancellationToken = default)
		{
			await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task<UpdateUserInfoResponse> Update(UpdateUserInfoCommand command, CancellationToken cancellationToken = default)
		{
			var entityToUpdate = await _dbContext.UserInfos
				.FirstOrDefaultAsync(e => e.Id == command.UpdateId, cancellationToken);

			if (entityToUpdate == null)
			{
				throw new BadRequestException("The userInfo you want to update does not exist.");
			}

			if (command.Name != null) { entityToUpdate.Name = command.Name; }
			if (command.Password != null) { entityToUpdate.Password = BCryptNet.HashPassword(command.Password); }
			if (command.Role != null) { entityToUpdate.Role = command.Role; }
			entityToUpdate.Token = Guid.NewGuid();

			await SaveChanges(cancellationToken);

			return new UpdateUserInfoResponse
			{
				UserId = entityToUpdate.Id,
				UserToken = entityToUpdate.Token,//command.UserId == command.UpdateId ? entityToUpdate.Token : null,
				UserRole = entityToUpdate.Role,
			};
		}
	}
}
