using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Services.API
{
    public interface IUserInfoService
	{
		public Task Add(string userName, string password);
		public Task Delete(Guid id);
		public Task Update(UserInfoDto userInfoDto, string? password);
		public Task<List<UserInfoDto>> GetAll();
		public Task<UserInfoDto> GetById(Guid id);
		public Task Login(string userName, string password);
	}
}
