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
        public ICollection<Journal> Journals { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<Routine> Routines { get; set; }
        public ICollection<Habit> Habits { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Goal> Goals{ get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<WeightMeasurement> WeightMeasurements { get; set; }
        public ICollection<BodyMeasurement> BodyMeasurements { get; set; }
    }
}
