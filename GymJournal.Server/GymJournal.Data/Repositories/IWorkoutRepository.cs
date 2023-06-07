using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.Queries.WorkoutQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
    public interface IWorkoutRepository
	{
		public Task<AddWorkoutResponse> Add(AddWorkoutCommand command, CancellationToken cancellationToken = default);
		public Task<UpdateWorkoutResponse> Update(UpdateWorkoutCommand command, CancellationToken cancellationToken = default);
		public Task Delete(DeleteWorkoutCommand command, CancellationToken cancellationToken = default);
		public Task<GetAllWorkoutResponse> GetAll(GetAllWorkoutQuery query, CancellationToken cancellationToken = default);
		public Task<GetByIdWorkoutResponse> GetById(GetByIdWorkoutQuery query, CancellationToken cancellationToken = default);
		Task SaveChanges(CancellationToken cancellationToken = default);
	}
}
