using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
    public interface IExerciseRepository
	{
		public Task<AddExerciseResponse> Add(AddExerciseCommand command, CancellationToken cancellationToken = default);
		public Task<UpdateExerciseResponse> Update(UpdateExerciseCommand command, CancellationToken cancellationToken = default);
		public Task Delete(DeleteExerciseCommand command, CancellationToken cancellationToken = default);
		public Task<GetAllExerciseResponse> GetAll(GetAllExerciseQuery query, CancellationToken cancellationToken = default);
		public Task<GetByIdExerciseResponse> GetById(GetByIdExerciseQuery query, CancellationToken cancellationToken = default);
		Task SaveChanges(CancellationToken cancellationToken = default);
	}
}
