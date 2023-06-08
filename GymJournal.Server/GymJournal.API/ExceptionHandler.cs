using GymJournal.API.Models;
using GymJournal.Data.RequestValidators.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GymJournal.API
{
	public class ExceptionHandler
	{
		public IActionResult HandleException(Exception ex)
		{
			if (ex is UnauthorizedAccessException)
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return new ObjectResult(errorResponse)
				{
					StatusCode = StatusCodes.Status401Unauthorized
				};
			}
			else if (ex is BadRequestException)
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return new ObjectResult(errorResponse)
				{
					StatusCode = StatusCodes.Status400BadRequest
				};
			}
			else if (ex is InvalidRequestException)
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return new ObjectResult(errorResponse)
				{
					StatusCode = StatusCodes.Status405MethodNotAllowed
				};
			}
			else
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return new ObjectResult(errorResponse)
				{
					StatusCode = StatusCodes.Status500InternalServerError
				};
			}
		}
	}
}
