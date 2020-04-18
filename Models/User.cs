using JournalMdServer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Models
{
    public class User : BaseEntity, IBaseModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        // Relations
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Note> Notes { get; set; }
        // public ICollection<NoteValue> NoteValues { get; set; } // Only access by Notes
    }
}
