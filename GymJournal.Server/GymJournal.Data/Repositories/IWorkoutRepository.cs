using GymJournal.Domain.Commands.ExerciseCommands;
using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public interface IWorkoutRepository
	{
		Task<WorkoutDto> GetById(Guid? guid, CancellationToken cancellationToken = default);
		Task<IEnumerable<WorkoutDto>> GetAll(CancellationToken cancellationToken = default);
		Task<WorkoutDto> Add(AddWorkoutCommand command, CancellationToken cancellationToken = default);
		Task<WorkoutDto> Update(UpdateWorkoutCommand command, CancellationToken cancellationToken = default);
		Task Remove(Guid? id, CancellationToken cancellationToken = default);
		Task SaveChanges(CancellationToken cancellationToken = default);
	}
}
