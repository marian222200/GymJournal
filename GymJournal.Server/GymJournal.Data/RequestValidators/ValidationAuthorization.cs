using GymJournal.Data.Context.IContext;
using GymJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GymJournal.Data.RequestValidators
{
	public class ValidationAuthorization : IValidationAuthorization
	{
		private readonly IApplicationDbContext _dbContext;

		public ValidationAuthorization(IApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}
		public async Task ValidateAdminUser(Guid UserId, Guid UserToken)
		{
			var user = await _dbContext.UserInfos.FirstOrDefaultAsync(u => u.Id == UserId);

			if (user == null)
			{
				throw new UnauthorizedAccessException("invalid UserId");
			}

			if (user.Token != UserToken)
			{
				throw new UnauthorizedAccessException("invalid UserToken");
			}

			if(user.Role != "Admin")
			{
				throw new UnauthorizedAccessException("unauthorized User");
			}
		}

		public async Task ValidateRegularUser(Guid UserId, Guid UserToken)
		{
			var user = await _dbContext.UserInfos.FirstOrDefaultAsync(u => u.Id == UserId);

			if (user == null)
			{
				throw new UnauthorizedAccessException("invalid UserId");
			}

			if (user.Token != UserToken)
			{
				throw new UnauthorizedAccessException("invalid UserToken");
			}
		}
	}
}
