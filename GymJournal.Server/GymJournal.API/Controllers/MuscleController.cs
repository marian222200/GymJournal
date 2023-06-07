using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Data.RequestValidators.Validators;
using GymJournal.Domain.Queries.MuscleQueries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MuscleController : ControllerBase
	{
		private readonly IMuscleRepository _muscleRepository;
		private readonly IMuscleValidators _muscleValidators;

		public MuscleController(IMuscleRepository muscleRepository, IMuscleValidators muscleValidators)
		{
			_muscleRepository = muscleRepository ?? throw new ArgumentNullException(nameof(muscleRepository));
			_muscleValidators = muscleValidators ?? throw new ArgumentNullException(nameof(muscleValidators));
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromBody] GetAllMuscleQuery query)
		{
			try
			{
				await _muscleValidators.Validate(query);

				var response = await _muscleRepository.GetAll(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetById([FromBody] GetByIdMuscleQuery query)
		{
			try
			{
				await _muscleValidators.Validate(query);

				var response = await _muscleRepository.GetById(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
			}
		}

		public IActionResult HandleException(Exception ex)
		{
			if (ex is UnauthorizedAccessException)
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return StatusCode(StatusCodes.Status401Unauthorized, errorResponse);
			}
			else if (ex is BadRequestException)
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return StatusCode(StatusCodes.Status400BadRequest, errorResponse);
			}
			else if (ex is InvalidRequestException)
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return StatusCode(StatusCodes.Status405MethodNotAllowed, errorResponse);
			}
			else
			{
				var errorResponse = new ErrorResponse
				{
					Message = ex.Message,
				};

				return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
			}
		}
	}
}
