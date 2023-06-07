using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Data.RequestValidators.Validators;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ExerciseController : ControllerBase
	{
		private readonly IExerciseRepository _exerciseRepository;
		private readonly IExerciseValidators _exerciseValidators;

		public ExerciseController(IExerciseRepository exerciseRepository, IExerciseValidators exerciseValidators)
		{
			_exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
			_exerciseValidators = exerciseValidators ?? throw new ArgumentNullException(nameof(exerciseValidators));
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery] GetAllExerciseQuery query)
		{
			try
			{
				await _exerciseValidators.Validate(query);

				var response = await _exerciseRepository.GetAll(query);

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
		public async Task<IActionResult> GetById([FromQuery] GetByIdExerciseQuery query)
		{
			try
			{
				await _exerciseValidators.Validate(query);

				var response = await _exerciseRepository.GetById(query);

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

		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete([FromBody] DeleteExerciseCommand command)
		{
			try
			{
				await _exerciseValidators.Validate(command);

				await _exerciseRepository.Delete(command);

				return StatusCode(StatusCodes.Status204NoContent);
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

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] AddExerciseCommand command)
		{
			try
			{
				await _exerciseValidators.Validate(command);

				var response = await _exerciseRepository.Add(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Created(string.Empty, serializedResponse);
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

		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdateExerciseCommand command)
		{
			try
			{
				await _exerciseValidators.Validate(command);

				var response = await _exerciseRepository.Update(command);

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
