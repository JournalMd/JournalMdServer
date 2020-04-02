using AutoMapper;
using JournalMdServer.DTOs.Notes;
using JournalMdServer.DTOs.Users;

namespace JournalMdServer.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Users
            CreateMap<RegisterInput, User>();
            CreateMap<User, RegisterOutput>();
            CreateMap<User, UserOutput>();

            // Notes
            CreateMap<Note, NoteOutput>();
            CreateMap<NoteInput, Note>();
        }
    }
}
