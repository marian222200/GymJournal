using GymJournal.App.Models;
using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.DTOs;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.WorkoutPlanQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public class WorkoutPlanService : IWorkoutPlanService
	{
		private readonly IdentityService _identityService;
		private readonly ConstantsService _constantsService;

		public WorkoutPlanService(IdentityService identityService, ConstantsService constantsService)
		{
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
			_constantsService = constantsService ?? throw new ArgumentNullException(nameof(constantsService));
		}

		public async Task<WorkoutPlanDto> AddExercise(WorkoutPlanDto workoutPlan)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/WorkoutPlan/Add"
			};
			var url = builder.Uri.ToString();

			var command = new AddWorkoutPlanCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				Name = workoutPlan.Name,
				Description = workoutPlan.Description,
				WorkoutIds = workoutPlan.Workouts.Select(w => w.Id).ToArray(),
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<AddWorkoutPlanResponse>();
				return new WorkoutPlanDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
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
				Path = "/WorkoutPlan/Delete"
			};
			var url = builder.Uri.ToString();

			var command = new DeleteWorkoutPlanCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				WorkoutPlanId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (!response.IsSuccessStatusCode)
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<List<WorkoutPlanDto>> GetAll()
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/WorkoutPlan/GetAll"
			};
			var url = builder.Uri.ToString();

			var query = new GetAllWorkoutPlanQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<GetAllWorkoutPlanResponse>();
				return responseObject.WorkoutPlans;
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<WorkoutPlanDto> GetById(Guid id)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/WorkoutPlan/GetById"
			};
			var url = builder.Uri.ToString();

			var query = new GetByIdWorkoutPlanQuery
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				WorkoutPlanId = id,
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<GetByIdWorkoutPlanResponse>();
				return new WorkoutPlanDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
					Workouts = responseObject.Workouts,
				};
			}
			else
			{
				throw new ServerRequestException(await response.Content.ReadAsStringAsync());
			}
		}

		public async Task<WorkoutPlanDto> Update(WorkoutPlanDto workoutPlan)
		{
			HttpClient httpClient = new HttpClient();

			UriBuilder builder = new UriBuilder(_constantsService.HostAddress)
			{
				Path = "/WorkoutPlan/Update"
			};
			var url = builder.Uri.ToString();

			var command = new UpdateWorkoutPlanCommand
			{
				UserId = _identityService.UserId,
				UserToken = _identityService.UserToken,
				WorkoutPlanId = workoutPlan.Id,
				Name = workoutPlan.Name,
				Description = workoutPlan.Description,
				WorkoutIds = workoutPlan.Workouts.Select(w => w.Id).ToArray(),
			};

			HttpContent content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

			var response = await httpClient.PutAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				var responseObject = await content.ReadFromJsonAsync<UpdateWorkoutPlanResponse>();
				return new WorkoutPlanDto
				{
					Id = responseObject.Id,
					Name = responseObject.Name,
					Description = responseObject.Description,
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
