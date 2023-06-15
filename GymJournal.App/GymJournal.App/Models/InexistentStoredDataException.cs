using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Models
{
	internal class InexistentStoredDataException : Exception
	{
		public InexistentStoredDataException() { }
		public InexistentStoredDataException(string message) : base(message) { }
		public InexistentStoredDataException(string message, Exception innerException) : base(message, innerException) { }
	}
}
