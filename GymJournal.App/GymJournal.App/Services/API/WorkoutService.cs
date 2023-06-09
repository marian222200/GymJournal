using GymJournal.App.Models;
using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.WorkoutPlanQueries;
using GymJournal.Domain.Queries.WorkoutQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public class WorkoutService : IWorkoutService
	{
		private readonly IdentityService _identityService;
		private readonly ConstantsService _constantsService;

		public WorkoutService(IdentityService identityService, ConstantsService constantsService)
		{
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_constantsService = constantsService ?? throw new ArgumentNullException(nameof(constantsService));
		}

		public async Task<WorkoutDto> AddExercise(WorkoutDto workout)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Workout/Add"
			};
			var url = builder.Uri.ToString();

			var command = new AddWorkoutCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				Name = workout.Name,
				Description = workout.Description,
				ExerciseIds = workout.Exercises.Select(e => e.Id).ToArray(),
				WorkoutPlanIds = workout.WorkoutPlans.Select(w => w.Id).ToArray(),
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<AddWorkoutResponse>();
				return new WorkoutDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
					Exercises = responseObject.Exercises,
					WorkoutPlans = responseObject.WorkoutPlans,
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
				Path = "/Workout/Delete"
			};
			var url = builder.Uri.ToString();

			var command = new DeleteWorkoutCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				WorkoutId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (!response.IsSuccessStatusCode)
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<List<WorkoutDto>> GetAll()
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Workout/GetAll"
			};
			var url = builder.Uri.ToString();

			var query = new GetAllWorkoutQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<GetAllWorkoutResponse>();
				return responseObject.Workouts;
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<WorkoutDto> GetById(Guid id)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Workout/GetById"
			};
			var url = builder.Uri.ToString();

			var query = new GetByIdWorkoutQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				WorkoutId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<GetByIdWorkoutResponse>();
				return new WorkoutDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
					Exercises = responseObject.Exercises,
					WorkoutPlans = responseObject.WorkoutPlans,
				};
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<WorkoutDto> Update(WorkoutDto workout)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/Workout/Update"
			};
			var url = builder.Uri.ToString();

			var command = new UpdateWorkoutCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				WorkoutId = workout.Id,
				Name = workout.Name,
				Description = workout.Description,
				ExerciseIds = workout.Exercises.Select(e => e.Id).ToArray(),
				WorkoutPlanIds = workout.WorkoutPlans.Select(w => w.Id).ToArray(),
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PutAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<UpdateWorkoutResponse>();
				return new WorkoutDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
					Exercises = responseObject.Exercises,
					WorkoutPlans = responseObject.WorkoutPlans,
				};
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}
	}
}
