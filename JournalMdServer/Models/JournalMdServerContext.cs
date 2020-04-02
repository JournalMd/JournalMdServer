using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using JournalMdServer.Interfaces.Models;

namespace JournalMdServer.Models
{
    public class JournalMdServerContext : DbContext, IJournalMdServerContext
    {
        private IDbContextTransaction _transaction;

        public JournalMdServerContext(DbContextOptions<JournalMdServerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Services.UsersService.CreatePasswordHash("12345678", out byte[] PasswordHash, out byte[] PasswordSalt);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, PasswordHash = PasswordHash, PasswordSalt = PasswordSalt, Username = "1", FirstName = "Max", LastName = "Mustermann" },
                new User { Id = 2, PasswordHash = PasswordHash, PasswordSalt = PasswordSalt, Username = "2" }
            );

            modelBuilder.Entity<Note>().HasData(
                new Note { Id = 1, Title = "Bla 1.1", UserId = 1 },
                new Note { Id = 2, Title = "Bla 1.2", UserId = 1 },
                new Note { Id = 3, Title = "Bla 1.3", UserId = 1 },

                new Note { Id = 4, Title = "Bla 2.1", UserId = 2 },
                new Note { Id = 5, Title = "Bla 2.2", UserId = 2 }
            );
        }
    }
}
