using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.InputValidatorService
{
	public interface IInputValidatorService
	{
		string ValidateUserName(string userName);
		string ValidatePassword(string password);
		string ValidateRepeatPassword(string password, string repeatPassword);
	}
}
