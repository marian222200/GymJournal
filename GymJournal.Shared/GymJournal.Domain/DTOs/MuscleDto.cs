﻿namespace GymJournal.Domain.DTOs
{
    public class MuscleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ExerciseDto> Exercises { get; set; }
    }
}