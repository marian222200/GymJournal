namespace GymJournal.Domain.Commands.WorkoutPlanCommands
{
	public class AddWorkoutPlanCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<Guid> WorkoutIds { get; set; }
	}
}
