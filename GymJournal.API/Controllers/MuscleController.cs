using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MuscleController : ControllerBase
	{
		private readonly IMuscleRepository _muscleRepository;

		public MuscleController(IMuscleRepository muscleRepository)
		{
			_muscleRepository = muscleRepository ?? throw new ArgumentNullException(nameof(muscleRepository));
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var muscles = await _muscleRepository.GetAll();

				return Ok(muscles);
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
					return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { Message = "Trying to GetById with null Id is invalid." });
				}

				var muscle = await _muscleRepository.GetById(id);

				return Ok(muscle);
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
