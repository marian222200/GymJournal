using AutoMapper;
using GymJournal.Data.Entities;
using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Mappers
{
	public class WorkoutPlanProfile : Profile
	{
		public WorkoutPlanProfile()
		{
			CreateMap<WorkoutPlan, WorkoutPlanDto>();
			CreateMap<WorkoutPlanDto, WorkoutPlan>();
		}
	}
}
