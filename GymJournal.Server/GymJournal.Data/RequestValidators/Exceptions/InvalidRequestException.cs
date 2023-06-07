using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.RequestValidators.Exceptions
{
	public class InvalidRequestException : Exception
	{
		public InvalidRequestException() { }
		public InvalidRequestException(string message) : base(message) { }
		public InvalidRequestException(string message, Exception innerException) : base(message, innerException) { }
	}
}
