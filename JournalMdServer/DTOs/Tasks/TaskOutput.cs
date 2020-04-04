using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalMdServer.Interfaces.DTOs;

namespace JournalMdServer.DTOs.Tasks
{
    public class TaskOutput : IBaseOutput
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public DateTime Due { get; set; }

        public string Labels { get; set; }
    }
}
