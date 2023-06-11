using GymJournal.App.Models;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.UserInfoCommands;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.UserInfoQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public class UserInfoService : IUserInfoService
	{
		private readonly IdentityService _identityService;
		private readonly ConstantsService _constantsService;

		public UserInfoService(IdentityService identityService, ConstantsService constantsService)
		{
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_constantsService = constantsService ?? throw new ArgumentNullException(nameof(constantsService));
		}

		public async Task Add(string userName, string password)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/UserInfo/Add"
			};
			var url = builder.Uri.ToString();

			var command = new AddUserInfoCommand
			{
				Name = userName,
				Password = password,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<AddUserInfoResponse>();

				_identityService.UserId = responseObject.UserId;
				_identityService.UserToken = responseObject.UserToken;
				_identityService.IsAuthenticated = true;
				_identityService.UserRole = responseObject.UserRole;
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task Delete(Guid id)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/UserInfo/Delete"
			};
			var url = builder.Uri.ToString();

			var command = new DeleteUserInfoCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				DeleteId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);
			if (response.IsSuccessStatusCode)
			{
				_identityService.IsAuthenticated = false;
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<List<UserInfoDto>> GetAll()
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/UserInfo/GetAll"
			};
			var url = builder.Uri.ToString();

			var query = new GetAllUserInfoQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<GetAllUserInfoResponse>();
				return responseObject.UserInfos;
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<UserInfoDto> GetById(Guid id)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/UserInfo/GetById"
			};
			var url = builder.Uri.ToString();

			var query = new GetByIdUserInfoQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				GetId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<GetByIdUserInfoResponse>();
				return new UserInfoDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Role = responseObject.Role,
				};
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task Login(string userName, string password)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/UserInfo/Login"
			};
			var url = builder.Uri.ToString();

			var command = new LoginUserInfoQuery
			{
				UserName = userName,
				UserPassword = password,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<LoginUserInfoResponse>();

				_identityService.UserId = responseObject.UserId;
				_identityService.UserToken = responseObject.UserToken;
				_identityService.IsAuthenticated = true;
				_identityService.UserRole = responseObject.UserRole;
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task Update(UserInfoDto userInfoDto, string password)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/UserInfo/Update"
			};
			var url = builder.Uri.ToString();

			var command = new UpdateUserInfoCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				UpdateId = userInfoDto.Id,
				Name = userInfoDto.Name,
				Password = password,
				Role = userInfoDto.Role,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<UpdateUserInfoResponse>();

				if (_identityService.UserRole != "Admin")
				{
					_identityService.UserId = responseObject.UserId;
					_identityService.UserToken = responseObject.UserToken;
					_identityService.UserRole = responseObject.UserRole;
				}
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}
	}
}
