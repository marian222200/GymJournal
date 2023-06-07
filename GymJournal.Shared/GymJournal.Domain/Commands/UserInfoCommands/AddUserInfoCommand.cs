using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.UserInfoCommands
{
	public class AddUserInfoCommand
	{
		public string Name { get; set; }
		public string Password { get; set; }
	}
}
