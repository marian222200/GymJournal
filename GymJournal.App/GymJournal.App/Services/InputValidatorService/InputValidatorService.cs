using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.InputValidatorService
{
	public class InputValidatorService : IInputValidatorService
	{
		public string ValidatePassword(string password)
		{
			if (password.Length < 10) return new string("Password is too short.");
			else return null;
		}

		public string ValidateRepeatPassword(string password, string repeatPassword)
		{
			if (password != repeatPassword) return new string("Passwords don't match.");
			else return null;
		}

		public string ValidateUserName(string userName)
		{
			if (userName.Length < 10) return new string("User Name is too short");
			else return null;
		}
	}
}
