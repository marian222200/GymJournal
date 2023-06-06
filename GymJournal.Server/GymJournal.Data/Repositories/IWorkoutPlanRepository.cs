using GymJournal.Domain.Commands.WorkoutCommands;
using GymJournal.Domain.Commands.WorkoutPlanCommands;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Repositories
{
	public interface IWorkoutPlanRepository
	{
		Task<WorkoutPlanDto> GetById(Guid? guid, CancellationToken cancellationToken = default);
		Task<IEnumerable<WorkoutPlanDto>> GetAll(CancellationToken cancellationToken = default);
		Task<WorkoutPlanDto> Add(AddWorkoutPlanCommand command, CancellationToken cancellationToken = default);
		Task<WorkoutPlanDto> Update(UpdateWorkoutPlanCommand command, CancellationToken cancellationToken = default);
		Task Remove(Guid? id, CancellationToken cancellationToken = default);
		Task SaveChanges(CancellationToken cancellationToken = default);
	}
}
