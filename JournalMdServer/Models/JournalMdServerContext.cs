using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using JournalMdServer.Interfaces.Models;
using System;

namespace JournalMdServer.Models
{
    public class JournalMdServerContext : DbContext, IJournalMdServerContext
    {
        private IDbContextTransaction _transaction;

        public JournalMdServerContext(DbContextOptions<JournalMdServerContext> options)
            : base(options)
        {
            Database.EnsureCreated(); // TODO Migrations on prod...
        }

        public DbSet<User> Users { get; set; }
        public DbSet<NoteType> NoteTypes { get; set; }
        public DbSet<NoteField> NoteFields { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
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

            modelBuilder.Entity<NoteType>().HasData(
                new NoteType { Id = 1, Order = 1, Name = "journal", Title = "Journal", Description = "Summarize your day or write down your thoughts." },
                new NoteType { Id = 2, Order = 2, Name = "note", Title = "Note", Description = "Write a simple note." },
                new NoteType { Id = 3, Order = 3, Name = "task", Title = "Task", Description = "A task you must complete." },
//                      { id: 1, order: 1, name: 'completed', title: 'Completed', description: 'Is it done?', type: 'boolean', showInViews: true },
//                      { id: 5, order: 2, name: 'due', title: 'Due', description: 'When is it due?', type: 'date', showInViews: true },
                new NoteType { Id = 4, Order = 4, Name = "goal", Title = "Goal", Description = "A goal you want to achieve." },
//                      { id: 2, order: 1, name: 'achieved', title: 'Achieved', description: 'Did you make it?', type: 'boolean', showInViews: true },
                new NoteType { Id = 5, Order = 5, Name = "activity", Title = "Activity", Description = "Something you've done." },
                new NoteType { Id = 6, Order = 6, Name = "habit", Title = "Habit", Description = "Record your habits." },
                new NoteType { Id = 7, Order = 7, Name = "routine", Title = "Routine", Description = "Write down what you want to do every day." },
                new NoteType { Id = 8, Order = 8, Name = "weightmeasurement", Title = "Weight Measurement", Description = "Track your weight." },
//                      { id: 3, order: 1, name: 'height', title: 'Height', description: 'Your height in cm.', type: 'number', showInViews: false },
//                      { id: 4, order: 2, name: 'weight', title: 'Weight', description: 'Your weight in kg.', type: 'number', showInViews: true },
                new NoteType { Id = 9, Order = 9, Name = "bodymeasurement", Title = "Body Measurement", Description = "Track your body measurements." }
            );

            modelBuilder.Entity<NoteField>().HasData(
                new NoteField { Id = 1, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 2, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 3, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 4, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 5, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 6, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 7, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 8, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 9, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 10, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 11, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 12, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 13, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 14, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 15, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 16, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 17, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 18, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 19, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 20, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 21, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 22, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 23, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" },
                new NoteField { Id = 24, NoteTypeId = 1, Order = 1, Name = "", Title = "", Description = "", Rules = "", Type = "" }
            );

            // LABELS )=> category
            /*
                *   const labels: any[] = [ // TODO strongly type
            // Auto Tagger weekday
            { id: 1, category: 'weekday', name: 'weekday_monday', title: 'Monday', owner: null, parent: null },
            { id: 2, category: 'weekday', name: 'weekday_tuesday', title: 'Tuesday', owner: null, parent: null },
            { id: 3, category: 'weekday', name: 'weekday_wednesday', title: 'Wednesday', owner: null, parent: null },
            { id: 4, category: 'weekday', name: 'weekday_thursday', title: 'Thursday', owner: null, parent: null },
            { id: 5, category: 'weekday', name: 'weekday_friday', title: 'Friday', owner: null, parent: null },
            { id: 6, category: 'weekday', name: 'weekday_saturday', title: 'Saturday', owner: null, parent: null },
            { id: 7, category: 'weekday', name: 'weekday_sunday', title: 'Sunday', owner: null, parent: null },
            // Auto Tagger time
            { id: 8, category: 'time', name: 'time_today', title: 'Today', owner: null, parent: null },
            { id: 9, category: 'time', name: 'time_week', title: 'Week', owner: null, parent: null },
            { id: 10, category: 'time', name: 'time_month', title: 'Month', owner: null, parent: null },
            { id: 11, category: 'time', name: 'time_year', title: 'Year', owner: null, parent: null },
            { id: 12, category: 'time', name: 'time_life', title: 'Life', owner: null, parent: null },
            // categorie
            { id: 13, category: 'categorie', name: 'categorie_quote', title: 'Quote', owner: null, parent: null },
            { id: 14, category: 'categorie', name: 'categorie_shopping_list', title: 'Shopping_list', owner: null, parent: null },
            { id: 15, category: 'categorie', name: 'categorie_xxx', title: 'xxx', owner: null, parent: null },
            { id: 16, category: 'categorie', name: 'categorie_yyy', title: 'yyy', owner: null, parent: null },
            { id: 17, category: 'categorie', name: 'categorie_zzz', title: 'zzz', owner: null, parent: null },
            // activity
            { id: 18, category: 'activity', name: 'activity_weight_training', title: 'Weight_training', owner: null, parent: null },
            { id: 19, category: 'activity', name: 'activity_running', title: 'Running', owner: null, parent: null },
            { id: 20, category: 'activity', name: 'activity_dancing', title: 'Dancing', owner: null, parent: null },
            // user
            { id: 21, category: 'categorie', name: 'user_user_a', title: 'User_a', owner: 1, parent: null },
            { id: 22, category: 'categorie', name: 'user_user_b', title: 'User_b', owner: 1, parent: null },
            { id: 23, category: 'categorie', name: 'user_user_c', title: 'User_c', owner: 1, parent: null },
            ];
            */

            // tags

            modelBuilder.Entity<Note>().HasData(
                new Note { Id = 1, Title = "Bla 1.1", UserId = 1, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Note { Id = 2, Title = "Bla 1.2", UserId = 1, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Note { Id = 3, Title = "Bla 1.3", UserId = 1, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },

                new Note { Id = 4, Title = "Bla 2.1", UserId = 2, CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Note { Id = 5, Title = "Bla 2.2", UserId = 2, CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );
        }
    }
}
