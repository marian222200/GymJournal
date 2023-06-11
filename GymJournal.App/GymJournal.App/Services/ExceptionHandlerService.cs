using GymJournal.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services
{
	public class ExceptionHandlerService
	{
		public async Task HandleException(Exception ex)
		{
			if (ex is ServerRequestException)
				await Shell.Current.DisplayAlert("Server Request Error", ex.Message, "OK");
		}
	}
}
