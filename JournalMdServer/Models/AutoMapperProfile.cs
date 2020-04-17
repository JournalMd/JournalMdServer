using AutoMapper;
using JournalMdServer.DTOs.Users;
using JournalMdServer.DTOs.NoteTypes;
using JournalMdServer.DTOs.NoteFields;
using JournalMdServer.DTOs.Categories;
using JournalMdServer.DTOs.Tags;
using JournalMdServer.DTOs.Notes;

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
