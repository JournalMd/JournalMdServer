using JournalMdServer.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Tags
{
    public class TagOutput : IBaseOutput
    {
        public long Id { get; set; }

        public string Name { get; set; } // intern technical

        public string Title { get; set; }
    }
}
