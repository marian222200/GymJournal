using GymJournal.App.Models;
using GymJournal.Domain.Commands.ExerciseCommands;
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

		public async Task<ExerciseDto> Add(ExerciseDto exercise)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Exercise/Add"
			};
			var url = builder.Uri.ToString();

			var command = new AddExerciseCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				Name = exercise.Name,
				Description = exercise.Description,
				MuscleIds = exercise.Muscles.Select(m => m.Id).ToArray(),
				WorkoutIds = exercise.Workouts.Select(w => w.Id).ToArray(),
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<AddExerciseResponse>();
				return new ExerciseDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
					Likes = responseObject.Likes,
					Muscles = responseObject.Muscles,
					Workouts = responseObject.Workouts,
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
				Path = "/Exercise/Delete"
			};
			var url = builder.Uri.ToString();

			var command = new DeleteExerciseCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				ExerciseId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (!response.IsSuccessStatusCode)
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<List<ExerciseDto>> GetAll()
        {
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Exercise/GetAll"
			};
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
				var responseObject = await content.ReadFromJsonAsync<GetAllExerciseResponse>();
                return responseObject.Exercises;
            }
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
        }

		public async Task<ExerciseDto> GetById(Guid id)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Exercise/GetById"
			};
			var url = builder.Uri.ToString();

			var query = new GetByIdExerciseQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				ExerciseId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<GetByIdExerciseResponse>();
				return new ExerciseDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
					Likes = responseObject.Likes,
					Muscles = responseObject.Muscles,
					Workouts = responseObject.Workouts,
				};
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<ExerciseDto> Update(ExerciseDto exercise)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Exercise/Update"
			};
			var url = builder.Uri.ToString();

			var command = new UpdateExerciseCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				ExerciseId = exercise.Id,
				Name = exercise.Name,
				Description = exercise.Description,
				Likes = exercise.Likes,
				MuscleIds = exercise.Muscles.Select(m => m.Id).ToArray(),
				WorkoutIds = exercise.Workouts.Select(w => w.Id).ToArray(),
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PutAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<UpdateExerciseResponse>();
				return new ExerciseDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
					Likes = responseObject.Likes,
					Muscles = responseObject.Muscles,
					Workouts = responseObject.Workouts,
				};
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}
	}
}
