using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WorkoutPlanController : ControllerBase
	{
		private readonly IRepository<WorkoutPlanDto> _workoutPlanRepository;

		public WorkoutPlanController(IRepository<WorkoutPlanDto> workoutPlanRepository)
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
		public async Task<IActionResult> Create([FromBody] WorkoutPlanDto workoutPlan)
		{
			try
			{
				if (workoutPlan == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to Add null WorkoutPlan." });
				}

				if (workoutPlan.WorkoutIds == null) workoutPlan.WorkoutIds = new List<Guid>();

				var responseWorkout = await _workoutPlanRepository.Add(workoutPlan);

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
		public async Task<IActionResult> Update([FromBody] WorkoutPlanDto workoutPlan)
		{
			try
			{
				var existentWorkoutPlan = await _workoutPlanRepository.GetById(workoutPlan.Id);

				if (existentWorkoutPlan == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to Update WorkoutPlan with not existent Id." });
				}

				var responseWorkoutPlan = await _workoutPlanRepository.Update(workoutPlan);

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