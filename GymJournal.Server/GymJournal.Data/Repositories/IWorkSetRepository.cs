using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkSetCommands;
using GymJournal.Domain.Queries.ExerciseQueries;
using GymJournal.Domain.Queries.WorkSetQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public interface IWorkSetRepository
	{
		public Task<AddWorkSetResponse> Add(AddWorkSetCommand command, CancellationToken cancellationToken = default);
		public Task Delete(DeleteWorkSetCommand command, CancellationToken cancellationToken = default);
		public Task<GetAllWorkSetResponse> GetAll(GetAllWorkSetQuery query, CancellationToken cancellationToken = default);
		public Task<GetByIdWorkSetResponse> GetById(GetByIdWorkSetQuery query, CancellationToken cancellationToken = default);
		Task SaveChanges(CancellationToken cancellationToken = default);
	}
}
