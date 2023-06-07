using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators
{
	public interface IValidationAuthorization
	{
		public Task ValidateRegularUser(Guid UserId, Guid UserToken);
		public Task ValidateAdminUser(Guid UserId, Guid UserToken);
	}
}
