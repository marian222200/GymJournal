using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.LocalStorageService
{
	public class UserIdStorageModel
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public bool IsAuthenticated { get; set; }
		public string UserRole { get; set; }
	}
}
