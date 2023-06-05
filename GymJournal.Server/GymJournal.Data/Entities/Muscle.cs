﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.Data.Entities
{
    public class Muscle
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<Exercise> Exercises { get; set; }
	}
}
