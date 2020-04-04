using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalMdServer.Interfaces.DTOs;

namespace JournalMdServer.DTOs.Routines
{
    public class RoutineOutput : IBaseOutput
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Labels { get; set; }
    }
}
