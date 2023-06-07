using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public interface IMuscleService
	{
		public Task<List<MuscleDto>> GetAll();
		public Task<MuscleDto> GetById(Guid id);
	}
}
