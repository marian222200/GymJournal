﻿using GymJournal.API.Models;
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
		private readonly ExceptionHandler _exceptionHandler;

		public WorkoutPlanController(IWorkoutPlanRepository workoutPlanRepository, IWorkoutPlanValidators workoutPlanValidators, ExceptionHandler exceptionHandler)
		{
			_workoutPlanRepository = workoutPlanRepository ?? throw new ArgumentNullException(nameof(workoutPlanRepository));
			_workoutPlanValidators = workoutPlanValidators ?? throw new ArgumentNullException(nameof(workoutPlanValidators));
			_exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
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
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetById([FromBody] GetByIdWorkoutPlanQuery query)
		{
			try
			{
				await _workoutPlanValidators.Validate(query);

				var response = await _workoutPlanRepository.GetById(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("Delete")]
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
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] AddWorkoutPlanCommand command)
		{
			try
			{
				await _workoutPlanValidators.Validate(command);

				var response = await _workoutPlanRepository.Add(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Created(string.Empty, serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdateWorkoutPlanCommand command)
		{
			try
			{
				await _workoutPlanValidators.Validate(command);

				var response = await _workoutPlanRepository.Update(command);

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