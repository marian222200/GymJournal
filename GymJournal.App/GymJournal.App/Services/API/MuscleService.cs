using GymJournal.App.Models;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.MuscleQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
	public class MuscleService : IMuscleService
	{
		private readonly IdentityService _identityService;
		private readonly ConstantsService _constantsService;

		public MuscleService(IdentityService identityService, ConstantsService constantsService)
		{
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_constantsService = constantsService ?? throw new ArgumentNullException(nameof(constantsService));
		}

		public async Task<List<MuscleDto>> GetAll()
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Muscle/GetAll"
			};
			var url = builder.Uri.ToString();

			var query = new GetAllMuscleQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var reponseObject = await content.ReadFromJsonAsync<GetAllMuscleResponse>();
				return reponseObject.Muscles;
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<MuscleDto> GetById(Guid id)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Muscle/GetById"
			};
			var url = builder.Uri.ToString();

			var query = new GetByIdMuscleQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				MuscleId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var reponseObject = await content.ReadFromJsonAsync<GetByIdMuscleResponse>();
				return new MuscleDto
				{
					Id = reponseObject.Id,
					Name = reponseObject.Name,
					Exercises = reponseObject.Exercises,
				};
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}
	}
}
