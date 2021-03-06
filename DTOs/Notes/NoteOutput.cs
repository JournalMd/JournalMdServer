﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalMdServer.DTOs.Categories;
using JournalMdServer.DTOs.NoteValues;
using JournalMdServer.DTOs.Tags;
using JournalMdServer.Interfaces.DTOs;

namespace JournalMdServer.DTOs.Notes
{
    public class NoteOutput : IBaseOutput
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Mood { get; set; }

        public DateTime Date { get; set; }

        // Relations
        public long NoteTypeId { get; set; }
        
        public ICollection<NoteValueOutput> NoteValues { get; set; }

        // Pseudo relation
        public ICollection<long> Categories { get; set; }

        public ICollection<long> Tags { get; set; }
    }
}
