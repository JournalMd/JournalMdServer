using Microsoft.EntityFrameworkCore;
using JournalMdServer.Models;

namespace JournalMdServer.Interfaces.Models
{
    interface IJournalMdServerContext
    {
        public interface IDataContext
        {
            // public DbSet<Note> Notes { get; set; }

            void BeginTransaction();
            void Commit();
            void Rollback();
        }
    }
}
