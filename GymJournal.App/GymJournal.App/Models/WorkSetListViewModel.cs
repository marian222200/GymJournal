using GymJournal.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymJournal.App.Models
{
    public class WorkSetListViewModel
    {
        public string Date { get; set; }
        public List<WorkSetDto> WorkSets { get; set; }
    }
}
