using AutoMapper;
using JournalMdServer.DTOs.Users;
using JournalMdServer.DTOs.NoteTypes;
using JournalMdServer.DTOs.NoteFields;
using JournalMdServer.DTOs.Categories;
using JournalMdServer.DTOs.Tags;
using JournalMdServer.DTOs.Activities;
using JournalMdServer.DTOs.BodyMeasurements;
using JournalMdServer.DTOs.Goals;
using JournalMdServer.DTOs.Habits;
using JournalMdServer.DTOs.Journals;
using JournalMdServer.DTOs.Notes;
using JournalMdServer.DTOs.Routines;
using JournalMdServer.DTOs.Tasks;
using JournalMdServer.DTOs.WeightMeasurements;

namespace JournalMdServer.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Users
            CreateMap<RegisterInput, User>();
            CreateMap<UserInput, User>();
            CreateMap<User, RegisterOutput>();
            CreateMap<User, UserOutput>();

            // NoteTypes
            CreateMap<NoteType, NoteTypeOutput>();

            // NoteFields
            CreateMap<NoteField, NoteFieldOutput> ();

            // Categories
            CreateMap<Category, CategoryOutput>();

            // Tags
            CreateMap<TagInput, Tag>();
            CreateMap<Tag, TagOutput>();

            // Notes
            CreateMap<NoteInput, Note>();
            CreateMap<Note, NoteOutput>();
        }
    }
}
