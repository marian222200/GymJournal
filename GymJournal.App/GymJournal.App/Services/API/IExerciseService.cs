using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public interface IExerciseService
    {
        public Task<ExerciseDto> Add(ExerciseDto exercise);
        public Task Delete(Guid id);
        public Task<ExerciseDto> Update(ExerciseDto exercise);
        public Task<List<ExerciseDto>> GetAll();
        public Task<ExerciseDto> GetById(Guid id);
    }
}
