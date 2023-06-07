using GymJournal.App.Models;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.ExerciseQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public class ExerciseService : IExerciseService
    {
		private readonly IdentityService _identityService;
		private readonly ConstantsService _constantsService;

		public ExerciseService(IdentityService identityService, ConstantsService constantsService)
		{
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_constantsService = constantsService ?? throw new ArgumentNullException(nameof(constantsService));
		}
		public Task<ExerciseDto> AddExercise(ExerciseDto exercise)
		{
			throw new NotImplementedException();
		}

		public Task Delete(ExerciseDto exercise)
		{
			throw new NotImplementedException();
		}

		public async Task<List<ExerciseDto>> GetAll()
        {
			HttpClient httpClient = new HttpClient();

            UriBuilder builder = new UriBuilder(_constantsService.HostAddress);
			builder.Path = "/Exercise/GetAll";
            var url = builder.Uri.ToString();

			var query = new GetAllExerciseQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ExerciseDto>>();
            }
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
        }

		public Task<ExerciseDto> GetById(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<ExerciseDto> Update(ExerciseDto exercise)
		{
			throw new NotImplementedException();
		}
	}
}
