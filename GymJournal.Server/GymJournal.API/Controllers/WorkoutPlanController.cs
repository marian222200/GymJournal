using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Data.RequestValidators.Validators;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.Queries.WorkoutPlanQueries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WorkoutPlanController : ControllerBase
	{
		private readonly IWorkoutPlanRepository _workoutPlanRepository;
		private readonly IWorkoutPlanValidators _workoutPlanValidators;

		public WorkoutPlanController(IWorkoutPlanRepository workoutPlanRepository, IWorkoutPlanValidators workoutPlanValidators)
		{
			_workoutPlanRepository = workoutPlanRepository ?? throw new ArgumentNullException(nameof(workoutPlanRepository));
			_workoutPlanValidators = workoutPlanValidators ?? throw new ArgumentNullException(nameof(workoutPlanValidators));
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromBody] GetAllWorkoutPlanQuery query)
		{
			try
			{
				await _workoutPlanValidators.Validate(query);

				var response = await _workoutPlanRepository.GetAll(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetById([FromBody] GetByIdWorkoutPlanQuery query)
		{
			try
			{
				await _workoutPlanValidators.Validate(query);

				var response = _workoutPlanRepository.GetById(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete([FromBody] DeleteWorkoutPlanCommand command)
		{
			try
			{
				await _workoutPlanValidators.Validate(command);

				await _workoutPlanRepository.Delete(command);

				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] AddWorkoutPlanCommand command)
		{
			try
			{
				await _workoutPlanValidators.Validate(command);

				var response = _workoutPlanRepository.Add(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Created(string.Empty, serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdateWorkoutPlanCommand command)
		{
			try
			{
				await _workoutPlanValidators.Validate(command);

				var response = _workoutPlanRepository.Update(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
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