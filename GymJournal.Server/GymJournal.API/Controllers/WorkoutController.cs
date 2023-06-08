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
		private readonly ExceptionHandler _exceptionHandler;

		public WorkoutController(IWorkoutRepository workoutRepository, IWorkoutValidators workoutValidators, ExceptionHandler exceptionHandler)
		{
			_workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
			_workoutValidators = workoutValidators ?? throw new ArgumentNullException(nameof(workoutValidators));
			_exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
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
				return _exceptionHandler.HandleException(ex);
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
				return _exceptionHandler.HandleException(ex);
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
				return _exceptionHandler.HandleException(ex);
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
				return _exceptionHandler.HandleException(ex);
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
				return _exceptionHandler.HandleException(ex);
			}
		}
	}
}
