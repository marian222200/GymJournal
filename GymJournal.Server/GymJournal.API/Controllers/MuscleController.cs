using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Data.RequestValidators.Exceptions;
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
		private readonly ExceptionHandler _exceptionHandler;

		public MuscleController(IMuscleRepository muscleRepository, IMuscleValidators muscleValidators, ExceptionHandler exceptionHandler)
		{
			_muscleRepository = muscleRepository ?? throw new ArgumentNullException(nameof(muscleRepository));
			_muscleValidators = muscleValidators ?? throw new ArgumentNullException(nameof(muscleValidators));
			_exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromBody] GetAllMuscleQuery query)
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
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetById([FromBody] GetByIdMuscleQuery query)
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
				return _exceptionHandler.HandleException(ex);
			}
		}
	}
}
