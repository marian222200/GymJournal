using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public interface IExerciseRepository
	{
		Task<ExerciseDto> GetById(Guid? guid, CancellationToken cancellationToken = default);
		Task<IEnumerable<ExerciseDto>> GetAll(CancellationToken cancellationToken = default);
		Task<ExerciseDto> Add(AddExerciseCommand command, CancellationToken cancellationToken = default);
		Task<ExerciseDto> Update(UpdateExerciseCommand command, CancellationToken cancellationToken = default);
		Task Remove(Guid? id, CancellationToken cancellationToken = default);
		Task SaveChanges(CancellationToken cancellationToken = default);
	}
}
