using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Domain.DTOs
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
		public Guid WorkoutPlanId { get; set; }
		public string WorkoutPlanStart { get; set; }
	}
}
