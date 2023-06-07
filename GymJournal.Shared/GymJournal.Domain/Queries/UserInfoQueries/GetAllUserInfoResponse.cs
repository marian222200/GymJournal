using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymJournal.Domain.DTOs;

namespace GymJournal.Domain.Queries.UserInfoQueries
{
    public class GetAllUserInfoResponse
    {
        public List<UserInfoDto> UserInfos { get; set; }
	}
}
