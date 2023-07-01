using GymJournal.API.Models;
using GymJournal.Data.Repositories;
using GymJournal.Data.RequestValidators.Exceptions;
using GymJournal.Data.RequestValidators.Validators;
using GymJournal.Domain.Commands.UserInfoCommands;
using GymJournal.Domain.Queries.UserInfoQueries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GymJournal.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserInfoController : ControllerBase
	{
		private readonly IUserInfoRepository _userInfoRepository;
		private readonly IUserInfoValidators _userInfoValidators;
		private readonly ExceptionHandler _exceptionHandler;

		public UserInfoController(IUserInfoRepository userInfoRepository, IUserInfoValidators userInfoValidators, ExceptionHandler exceptionHandler)
		{
			_userInfoRepository = userInfoRepository ?? throw new ArgumentNullException(nameof(userInfoRepository));
			_userInfoValidators = userInfoValidators ?? throw new ArgumentNullException(nameof(userInfoValidators));
			_exceptionHandler = exceptionHandler ?? throw new ArgumentNullException(nameof(exceptionHandler));
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll([FromBody] GetAllUserInfoQuery query)
		{
			try
			{
				await _userInfoValidators.Validate(query);

				var response = await _userInfoRepository.GetAll(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetById([FromBody] GetByIdUserInfoQuery query)
		{
			try
			{
				await _userInfoValidators.Validate(query);

				var response = await _userInfoRepository.GetById(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete([FromBody] DeleteUserInfoCommand command)
		{
			try
			{
				await _userInfoValidators.Validate(command);

				await _userInfoRepository.Delete(command);

				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] AddUserInfoCommand command)
		{
			try
			{
				await _userInfoValidators.Validate(command);

				var response = await _userInfoRepository.Add(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Created(string.Empty, serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdateUserInfoCommand command)
		{
			try
			{
				await _userInfoValidators.Validate(command);

				var response = await _userInfoRepository.Update(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginUserInfoQuery query)
		{
			try
			{
				await _userInfoValidators.Validate(query);

				var response = await _userInfoRepository.Login(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}
		[HttpPost("ChangeWorkoutPlan")]
		public async Task<IActionResult> ChangeWorkoutPlan([FromBody] ChangeWorkoutPlanCommand command)
		{
			try
			{
				await _userInfoValidators.Validate(command);

				await _userInfoRepository.ChangeWorkoutPlan(command);

				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return _exceptionHandler.HandleException(ex);
			}
		}
	}
}
