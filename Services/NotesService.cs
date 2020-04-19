using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Notes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class NotesService: BaseCRUDService<Note, NoteInput, NoteOutput>
    {
        public NotesService(IRepository<Note> repository, IMapper mapper): base(repository, mapper)
        {
        }

        public override async Task<ActionResult<IEnumerable<NoteOutput>>> GetAll(long userId)
        {
            var entities = await _repository.Query.Include(inc => inc.NoteValues).Where(m => m.UserId == userId).ToListAsync();
            return _mapper.Map<List<NoteOutput>>(entities);
        }

        // Intentionally left empty

        // TODO Rules Parser
        // TODO Calculator

        // TODO move this code to calculator
        /*

        /// <summary>	
        /// Calculated	
        /// https://de.wikipedia.org/wiki/Body-Mass-Index	
        /// </summary>	
        public double? BodyMassIndex	
        {	
            get	
            {	
                if (Weight <= 0 || Height <= 0)	
                    return null;	

                return Weight / (Height * Height) * 10000.0;	
            }	
        }	

        /// <summary>	
        /// Calculated	
        /// https://de.wikipedia.org/wiki/Ponderal-Index	
        /// </summary>	
        public double? PonderalIndex	
        {	
            get	
            {	
                if (Weight <= 0 || Height <= 0)	
                    return null;	

                return Weight / (Height * Height * Height) * 1000000.0;	
            }	
        }	
    
        /// <summary>	
        /// Calculated	
        /// https://en.wikipedia.org/wiki/Waist%E2%80%93hip_ratio	
        /// https://de.wikipedia.org/wiki/Taille-H%C3%BCft-Verh%C3%A4ltnis	
        /// </summary>	
        public double? WaistToHipRatio	
        {  	
            get	
            {	
                if (Waist == null || Hips == null || Waist <= 0 || Hips <= 0)	
                    return null;	

                return Waist / Hips;	
            } 	
        }    
        */
    }
}
