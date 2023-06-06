using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services
{
	public class ExerciseService
	{
		HttpClient httpClient { get; set; }

		List<ExerciseDto> exerciseList = new();

		public ExerciseService()
		{
			httpClient = new HttpClient();
		}

		public async Task<List<ExerciseDto>> GetAll()
		{
			if(exerciseList?.Count >0)
				return exerciseList;

			var url = "http://10.0.2.2:8080/Exercise";

			var response = await httpClient.GetAsync(url);

			if(response.IsSuccessStatusCode)
			{
				exerciseList = await response.Content.ReadFromJsonAsync<List<ExerciseDto>>();
			}
			return exerciseList;
		}
	}
}
