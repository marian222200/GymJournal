using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ExerciseController : ControllerBase
	{
		private readonly IExerciseRepository _exerciseRepository;

		public ExerciseController(IExerciseRepository exerciseRepository)
		{
			_exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var exercises = await _exerciseRepository.GetAll();

				return Ok(exercises);
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
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to GetById Exercise with null Id is invalid." });
				}

				var exercise = await _exerciseRepository.GetById(id);

				return Ok(exercise);
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
				var exercise = await _exerciseRepository.GetById(id);

				if(exercise == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to Delete Exercise with invalid Id." });
				}

				await _exerciseRepository.Remove(id);

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
		public async Task<IActionResult> Create([FromBody] AddExerciseCommand command)
		{
			try
			{
				if (command == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to Add null Exercise." });
				}

				if (command.MuscleIds == null) command.MuscleIds = new List<Guid>();
				if (command.WorkoutIds == null) command.WorkoutIds = new List<Guid>();

				var responseExercise = await _exerciseRepository.Add(command);

				return Created(string.Empty, responseExercise);
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
		public async Task<IActionResult> Update([FromBody] UpdateExerciseCommand command)
		{
			try
			{
				var existentExercise = await _exerciseRepository.GetById(command.Id);

				if(existentExercise == null)
				{
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to Update Exercise with not existent Id." });
				}

				var responseExercise = await _exerciseRepository.Update(command);

				return Ok(responseExercise);
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
