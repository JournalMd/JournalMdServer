using AutoMapper;
using JournalMdServer.DTOs.Users;
using JournalMdServer.DTOs.NoteTypes;
using JournalMdServer.DTOs.NoteFields;
using JournalMdServer.DTOs.Categories;
using JournalMdServer.DTOs.Tags;
using JournalMdServer.DTOs.Notes;
using JournalMdServer.DTOs.NoteValues;

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
            CreateMap<NoteField, NoteFieldOutput>()
                .ForMember(ign => ign.NoteType, opt => opt.Ignore()); // Ignore NoteType as NoteTypes Service will load this with include causing an object cycle

            // Categories
            CreateMap<Category, CategoryOutput>();

            // Tags
            CreateMap<TagInput, Tag>();
            CreateMap<Tag, TagOutput>();

            // Notes
            CreateMap<NoteInput, Note>();
            CreateMap<Note, NoteOutput>();

            // NoteValues
            // CreateMap<NoteValueInput, NoteValue>();
            CreateMap<NoteValue, NoteValueOutput>();
        }
    }
}
