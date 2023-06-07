namespace GymJournal.Domain.Commands.WorkoutCommands
{
	public class AddWorkoutCommand
	{
		public Guid UserId { get; set; }
		public Guid UserToken { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<Guid> ExerciseIds { get; set; }
		public ICollection<Guid> WorkoutPlanIds { get; set; }
	}
}
