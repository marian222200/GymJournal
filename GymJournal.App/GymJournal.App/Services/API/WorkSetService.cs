using GymJournal.App.Models;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkSetCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.WorkSetQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
	public class WorkSetService : IWorkSetService
	{
		private readonly IdentityService _identityService;
		private readonly ConstantsService _constantsService;

		public WorkSetService(IdentityService identityService, ConstantsService constantsService)
		{
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_constantsService = constantsService ?? throw new ArgumentNullException(nameof(constantsService));
		}

		public async Task<WorkSetDto> Add(WorkSetDto workSet)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/WorkSet/Create"
			};
			var url = builder.Uri.ToString();

			var command = new AddWorkSetCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				ExerciseId = workSet.ExerciseId,
				Date = DateTime.Now.ToString(),
				Weight = workSet.Weight,
				Reps = workSet.Reps,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await response.Content.ReadFromJsonAsync<AddWorkSetResponse>();
				return new WorkSetDto
				{
					Id = responseObject.Id,
					Date = responseObject.Date,
					Weight = responseObject.Weight,
					Reps = responseObject.Reps,
					ExerciseId = responseObject.ExerciseId,
					UserId = responseObject.UserId,
				};
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
				Path = "/WorkSet/Delete"
			};
			var url = builder.Uri.ToString();

			var command = new DeleteWorkSetCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				WorkSetId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (!response.IsSuccessStatusCode)
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<List<WorkSetDto>> GetAll()
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/WorkSet/GetAll"
			};
			var url = builder.Uri.ToString();

			var query = new GetAllWorkSetQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await response.Content.ReadFromJsonAsync<GetAllWorkSetResponse>();
				return responseObject.WorkSets.OrderBy(e => DateTime.Parse(e.Date)).ToList();
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<WorkSetDto> GetById(Guid id)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/WorkSet/GetById"
			};
			var url = builder.Uri.ToString();

			var query = new GetByIdWorkSetQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				WorkSetId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await response.Content.ReadFromJsonAsync<GetByIdWorkSetResponse>();
				return new WorkSetDto
				{
					Id = responseObject.Id,
					Date = responseObject.Date,
					Weight = responseObject.Weight,
					Reps = responseObject.Reps,
					ExerciseId = responseObject.ExerciseId,
					UserId = responseObject.UserId,
				};
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}
	}
}
