using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Models
{
	public class ServerRequestException : Exception
	{
		public ServerRequestException() { }
		public ServerRequestException(string message) : base(message) { }
		public ServerRequestException(string message, Exception innerException) : base(message, innerException) { }
	}
}
