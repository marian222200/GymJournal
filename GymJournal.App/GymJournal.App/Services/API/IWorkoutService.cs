using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public interface IWorkoutService
	{
		public Task<WorkoutDto> Add(WorkoutDto workout);
		public Task Delete(Guid id);
		public Task<WorkoutDto> Update(WorkoutDto workout);
		public Task<List<WorkoutDto>> GetAll();
		public Task<WorkoutDto> GetById(Guid id);
	}
}
