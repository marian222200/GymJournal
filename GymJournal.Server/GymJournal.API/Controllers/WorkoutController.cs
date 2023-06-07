using GymJournal.API.Models;
using GymJournal.Data.Entities;
using GymJournal.Data.Repositories;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Data.RequestValidators.Validators;
using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.Queries.WorkoutQueries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WorkoutController : ControllerBase
	{
		private readonly IWorkoutRepository _workoutRepository;
		private readonly IWorkoutValidators _workoutValidators;

		public WorkoutController(IWorkoutRepository workoutRepository, IWorkoutValidators workoutValidators)
		{
			_workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
			_workoutValidators = workoutValidators ?? throw new ArgumentNullException(nameof(workoutValidators));
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromBody] GetAllWorkoutQuery query)
		{
			try
			{
				await _workoutValidators.Validate(query);

				var response = await _workoutRepository.GetAll(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetById([FromBody] GetByIdWorkoutQuery query)
		{
			try
			{
				await _workoutValidators.Validate(query);

				var response = _workoutRepository.GetById(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete([FromBody] DeleteWorkoutCommand command)
		{
			try
			{
				await _workoutValidators.Validate(command);

				await _workoutRepository.Delete(command);

				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] AddWorkoutCommand command)
		{
			try
			{
				await _workoutValidators.Validate(command);

				var response = _workoutRepository.Add(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Created(string.Empty, serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdateWorkoutCommand command)
		{
			try
			{
				await _workoutValidators.Validate(command);

				var response = await _workoutRepository.Update(command);

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
