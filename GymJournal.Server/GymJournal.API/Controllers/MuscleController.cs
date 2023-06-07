using GymJournal.API.Models;
using GymJournal.Data.Repositories;
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

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery] GetAllMuscleQuery query)
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

		[HttpGet("GetById")]
		public async Task<IActionResult> GetById([FromQuery] GetByIdMuscleQuery query)
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
	}
}
