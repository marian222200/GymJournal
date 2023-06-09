﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.Commands.WorkoutPlanCommands
{
	public class UpdateWorkoutPlanCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public Guid WorkoutPlanId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public ICollection<Guid>? WorkoutIds { get; set; }
	}
}
