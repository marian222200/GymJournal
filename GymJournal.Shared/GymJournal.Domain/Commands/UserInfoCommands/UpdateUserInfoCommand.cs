using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.UserInfoCommands
{
	public class UpdateUserInfoCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid UpdateId { get; set; }
		public string? Name { get; set; }
		public string? Password { get; set; }
		public string? Role { get; set; }

	}
}
