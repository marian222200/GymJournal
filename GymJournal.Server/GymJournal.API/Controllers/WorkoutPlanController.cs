using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WorkoutPlanController : ControllerBase
	{
		private readonly IWorkoutPlanRepository _workoutPlanRepository;

		public WorkoutPlanController(IWorkoutPlanRepository workoutPlanRepository)
		{
			_workoutPlanRepository = workoutPlanRepository ?? throw new ArgumentNullException(nameof(workoutPlanRepository));
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var workoutPlans = await _workoutPlanRepository.GetAll();

				return Ok(workoutPlans);
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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid? id)
		{
			try
			{
				if (id == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to GetById WorkoutPlan with null Id is invalid." });
				}

				var workoutPlan = await _workoutPlanRepository.GetById(id);

				return Ok(workoutPlan);
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

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid? id)
		{
			try
			{
				var workoutPlan = await _workoutPlanRepository.GetById(id);

				if (workoutPlan == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to Delete WorkoutPlan with invalid Id." });
				}

				await _workoutPlanRepository.Remove(id);

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

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddWorkoutPlanCommand command)
		{
			try
			{
				if (command == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to Add null WorkoutPlan." });
				}

				if (command.WorkoutIds == null) command.WorkoutIds = new List<Guid>();

				var responseWorkout = await _workoutPlanRepository.Add(command);

				return Created(string.Empty, responseWorkout);
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

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateWorkoutPlanCommand command)
		{
			try
			{
				var existentWorkoutPlan = await _workoutPlanRepository.GetById(command.Id);

				if (existentWorkoutPlan == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to Update WorkoutPlan with not existent Id." });
				}

				var responseWorkoutPlan = await _workoutPlanRepository.Update(command);

				return Ok(responseWorkoutPlan);
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