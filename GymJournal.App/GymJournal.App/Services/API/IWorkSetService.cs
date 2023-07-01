using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public interface IWorkSetService
	{
		public Task<WorkSetDto> Add(WorkSetDto workSet);
		public Task Delete(Guid id);
		public Task<List<WorkSetDto>> GetAll();
		public Task<WorkSetDto> GetById(Guid id);
	}
}
