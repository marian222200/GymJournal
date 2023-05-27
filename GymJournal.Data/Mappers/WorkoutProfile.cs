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
	public class WorkoutProfile : Profile
	{
		public WorkoutProfile()
		{
			CreateMap<Workout, WorkoutDto>();
			CreateMap<WorkoutDto, Workout>();
		}
	}
}
