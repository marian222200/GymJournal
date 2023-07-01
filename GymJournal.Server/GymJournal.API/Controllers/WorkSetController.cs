using GymJournal.Data.Repositories;
using GymJournal.Data.RequestValidators.Validators;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkSetCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.WorkSetQueries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WorkSetController : ControllerBase
	{
		private readonly IWorkSetRepository _workSetRepository;
		private readonly IWorkSetValidators _workSetValidators;
		private readonly ExceptionHandler _exceptionHandler;

		public WorkSetController(IWorkSetRepository workSetRepository, IWorkSetValidators workSetValidators, ExceptionHandler exceptionHandler)
		{
			_workSetRepository = workSetRepository ?? throw new ArgumentNullException(nameof(workSetRepository));
			_workSetValidators = workSetValidators ?? throw new ArgumentNullException(nameof(workSetValidators));
			_exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromBody] GetAllWorkSetQuery query)
		{
			try
			{
				await _workSetValidators.Validate(query);

				var response = await _workSetRepository.GetAll(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetById([FromBody] GetByIdWorkSetQuery query)
		{
			try
			{
				await _workSetValidators.Validate(query);

				var response = await _workSetRepository.GetById(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete([FromBody] DeleteWorkSetCommand command)
		{
			try
			{
				await _workSetValidators.Validate(command);

				await _workSetRepository.Delete(command);

				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] AddWorkSetCommand command)
		{
			try
			{
				await _workSetValidators.Validate(command);

				var response = await _workSetRepository.Add(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Created(string.Empty, serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}
	}
}
