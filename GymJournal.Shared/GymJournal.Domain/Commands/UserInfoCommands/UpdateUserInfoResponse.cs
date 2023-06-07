using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.UserInfoCommands
{
	public class UpdateUserInfoResponse
	{
		public Guid UserId { get; set; }
		public Guid? Token { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
	}
}
