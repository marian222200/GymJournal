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

		public UserInfoController(IUserInfoRepository userInfoRepository, IUserInfoValidators userInfoValidators)
		{
			_userInfoRepository = userInfoRepository ?? throw new ArgumentNullException(nameof(userInfoRepository));
			_userInfoValidators = userInfoValidators ?? throw new ArgumentNullException(nameof(userInfoValidators));
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
				return HandleException(ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetById([FromBody] GetByIdUserInfoQuery query)
		{
			try
			{
				await _userInfoValidators.Validate(query);

				var response = _userInfoRepository.GetById(query);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpDelete("Delete")]
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
				return HandleException(ex);
			}
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] AddUserInfoCommand command)
		{
			try
			{
				await _userInfoValidators.Validate(command);

				var response = _userInfoRepository.Add(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Created(string.Empty, serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdateUserInfoCommand command)
		{
			try
			{
				await _userInfoValidators.Validate(command);

				var response = _userInfoRepository.Update(command);

				var serializedResponse = JsonSerializer.Serialize(response);

				return Ok(serializedResponse);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginUserInfoQuery query)
		{
			try
			{
				await _userInfoValidators.Validate(query);

				var response = _userInfoRepository.Login(query);

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
