using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public interface IWorkoutPlanService
	{
		public Task<WorkoutPlanDto> AddExercise(WorkoutPlanDto workoutPlan);
		public Task Delete(Guid id);
		public Task<WorkoutPlanDto> Update(WorkoutPlanDto workoutPlan);
		public Task<List<WorkoutPlanDto>> GetAll();
		public Task<WorkoutPlanDto> GetById(Guid id);
	}
}
