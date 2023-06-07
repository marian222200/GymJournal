using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.Queries.WorkoutPlanQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
    public interface IWorkoutPlanRepository
	{
		public Task<AddWorkoutPlanResponse> Add(AddWorkoutPlanCommand command, CancellationToken cancellationToken = default);
		public Task<UpdateWorkoutPlanResponse> Update(UpdateWorkoutPlanCommand command, CancellationToken cancellationToken = default);
		public Task Delete(DeleteWorkoutPlanCommand command, CancellationToken cancellationToken = default);
		public Task<GetAllWorkoutPlanResponse> GetAll(GetAllWorkoutPlanQuery query, CancellationToken cancellationToken = default);
		public Task<GetByIdWorkoutPlanResponse> GetById(GetByIdWorkoutPlanQuery query, CancellationToken cancellationToken = default);
		Task SaveChanges(CancellationToken cancellationToken = default);
	}
}
