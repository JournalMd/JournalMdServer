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
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
            modelBuilder.Entity<NoteTag>()
                .HasKey(t => new { t.NoteId, t.TagId });

            modelBuilder.Entity<NoteTag>()
                .HasOne(pt => pt.Note)
                .WithMany(p => p.NoteTags)
                .HasForeignKey(pt => pt.NoteId);

            modelBuilder.Entity<NoteTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.NoteTags)
                .HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<NoteCategory>()
                .HasKey(t => new { t.NoteId, t.CategoryId });

            modelBuilder.Entity<NoteCategory>()
                .HasOne(pt => pt.Note)
                .WithMany(p => p.NoteCategories)
                .HasForeignKey(pt => pt.NoteId);

            modelBuilder.Entity<NoteCategory>()
                .HasOne(pt => pt.Category)
                .WithMany(t => t.NoteCategories)
                .HasForeignKey(pt => pt.CategoryId);

            Services.UsersService.CreatePasswordHash("12345678", out byte[] PasswordHash, out byte[] PasswordSalt);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, PasswordHash = PasswordHash, PasswordSalt = PasswordSalt, Username = "1", FirstName = "Max", LastName = "Power" },
                new User { Id = 2, PasswordHash = PasswordHash, PasswordSalt = PasswordSalt, Username = "2" }
            );

            modelBuilder.Entity<NoteType>().HasData(
                new NoteType { Id = 1, Order = 1, Name = "journal", Title = "Journal", Description = "Summarize your day or write down your thoughts." },
                new NoteType { Id = 2, Order = 2, Name = "note", Title = "Note", Description = "Write a simple note." },
                new NoteType { Id = 3, Order = 3, Name = "task", Title = "Task", Description = "A task you must complete." },
                new NoteType { Id = 4, Order = 4, Name = "goal", Title = "Goal", Description = "A goal you want to achieve." },
                new NoteType { Id = 5, Order = 5, Name = "activity", Title = "Activity", Description = "Something you've done." },
                new NoteType { Id = 6, Order = 6, Name = "habit", Title = "Habit", Description = "Record your habits." },
                new NoteType { Id = 7, Order = 7, Name = "routine", Title = "Routine", Description = "Write down what you want to do every day." },
                new NoteType { Id = 8, Order = 8, Name = "weightmeasurement", Title = "Weight Measurement", Description = "Track your weight." },
                new NoteType { Id = 9, Order = 9, Name = "bodymeasurement", Title = "Body Measurement", Description = "Track your body measurements." }
            );

            modelBuilder.Entity<NoteField>().HasData(
                // Journal
                new NoteField { Id = 1, NoteTypeId = 1, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 2, NoteTypeId = 1, Order = 2, Name = "description", Title = "Description", Description = "", Required = true, Rules = "", Type = "markdown" },
                new NoteField { Id = 3, NoteTypeId = 1, Order = 3, Name = "mood", Title = "Mood", Description = "", Required = true, Rules = "", Type = "mood" },
                // Note
                new NoteField { Id = 4, NoteTypeId = 2, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 5, NoteTypeId = 2, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                // Task
                new NoteField { Id = 6, NoteTypeId = 3, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 7, NoteTypeId = 3, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 8, NoteTypeId = 3, Order = 3, Name = "completed", Title = "Completed", Description = "Is it done?", Required = true, Rules = "", Type = "boolean" },
                new NoteField { Id = 9, NoteTypeId = 3, Order = 4, Name = "due", Title = "Due", Description = "When is it due?", Required = false, Rules = "", Type = "datetime" },
                // Goal
                new NoteField { Id = 10, NoteTypeId = 4, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 11, NoteTypeId = 4, Order = 2, Name = "description", Title = "", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 12, NoteTypeId = 4, Order = 3, Name = "achieved", Title = "Completed", Description = "Did you make it?", Required = true, Rules = "", Type = "boolean" },
                new NoteField { Id = 13, NoteTypeId = 4, Order = 4, Name = "due", Title = "Due", Description = "When is it due?", Required = false, Rules = "", Type = "datetime" },
                // Activity
                new NoteField { Id = 14, NoteTypeId = 5, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 15, NoteTypeId = 5, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 16, NoteTypeId = 5, Order = 3, Name = "mood", Title = "Mood", Description = "", Required = true, Rules = "", Type = "mood" },
                // Habit
                new NoteField { Id = 17, NoteTypeId = 6, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 18, NoteTypeId = 6, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                // Routine
                new NoteField { Id = 19, NoteTypeId = 7, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 20, NoteTypeId = 7, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                // Weightmeasurement (no title)
                new NoteField { Id = 21, NoteTypeId = 8, Order = 1, Name = "shortdescription", Title = "Short Description", Description = "", Required = false, Rules = "", Type = "markdownsingle" },
                new NoteField { Id = 22, NoteTypeId = 8, Order = 2, Name = "weight", Title = "Weight", Description = "Your weight in kg.", Required = true, Rules = "", Type = "number" },
                new NoteField { Id = 23, NoteTypeId = 8, Order = 3, Name = "height", Title = "Height", Description = "Your height in cm.", Required = true, Rules = "default=previous", Type = "number" },
                new NoteField { Id = 24, NoteTypeId = 8, Order = 4, Name = "goalweight", Title = "Goal Weight", Description = "", Required = false, Rules = "default=previous", Type = "number" },
                new NoteField { Id = 25, NoteTypeId = 8, Order = 5, Name = "bodymassindex", Title = "Body-Mass-Index", Description = "", Required = false, Rules = "calculation=bodymassindex", Type = "calculated" },
                new NoteField { Id = 26, NoteTypeId = 8, Order = 6, Name = "ponderalindex", Title = "Ponderal-Index", Description = "", Required = false, Rules = "calculation=ponderalindex", Type = "calculated" },
                // Bodymeasurement (no title)
                new NoteField { Id = 27, NoteTypeId = 9, Order = 1, Name = "shortdescription", Title = "Short Description", Description = "", Required = false, Rules = "", Type = "markdownsingle" },
                new NoteField { Id = 28, NoteTypeId = 9, Order = 2, Name = "chest", Title = "Chest", Description = "", Required = false, Rules = "", Type = "number" }, // Brustumfang
                new NoteField { Id = 29, NoteTypeId = 9, Order = 3, Name = "waist", Title = "Waist", Description = "", Required = true, Rules = "", Type = "number" }, // Taillenumfang
                new NoteField { Id = 30, NoteTypeId = 9, Order = 4, Name = "hips", Title = "Hips", Description = "", Required = true, Rules = "", Type = "number" }, // Hüftumfang
                new NoteField { Id = 31, NoteTypeId = 9, Order = 5, Name = "arm", Title = "Arm", Description = "Larger biceps", Required = false, Rules = "", Type = "number" }, // Oberarm/Biceps
                new NoteField { Id = 32, NoteTypeId = 9, Order = 6, Name = "leg", Title = "Leg", Description = "Larger hamstrings/quadriceps", Required = false, Rules = "", Type = "number" }, // Oberschenkel
                new NoteField { Id = 33, NoteTypeId = 9, Order = 7, Name = "calf", Title = "Calf", Description = "Larger calf", Required = false, Rules = "", Type = "number" },// Waden
                new NoteField { Id = 34, NoteTypeId = 9, Order = 8, Name = "bodyfatmass", Title = "Body-Fat-Mass", Description = "", Required = false, Rules = "", Type = "number" }, // Körperfettanteil
                new NoteField { Id = 35, NoteTypeId = 9, Order = 9, Name = "bodyfatpercentage", Title = "Body-Fat-Percentage", Description = "", Required = false, Rules = "", Type = "number" }, // Body Fat %
                new NoteField { Id = 36, NoteTypeId = 9, Order = 10, Name = "totalbodywater", Title = "Total-Body-Water", Description = "", Required = false, Rules = "", Type = "number" }, // Wasseranteil
                new NoteField { Id = 37, NoteTypeId = 9, Order = 11, Name = "musclemass", Title = "Muscle-Mass", Description = "", Required = false, Rules = "", Type = "number" }, // Muskelanteil
                new NoteField { Id = 38, NoteTypeId = 9, Order = 12, Name = "waisttohipratio", Title = "Waist-To-Hip-Ratio", Description = "", Required = false, Rules = "calculation=waisttohipratio", Type = "calculated" }
            );

            modelBuilder.Entity<Category>().HasData(
                // Weekday
                new Category { Id = 1, Name = "weekday", Title = "Weekday" },
                new Category { Id = 2, Name = "weekday_monday", Title = "Monday", ParentCategoryId = 1 },
                new Category { Id = 3, Name = "weekday_tuesday", Title = "Tuesday", ParentCategoryId = 1 },
                new Category { Id = 4, Name = "weekday_wednesday", Title = "Wednesday", ParentCategoryId = 1 },
                new Category { Id = 5, Name = "weekday_thursday", Title = "Thursday", ParentCategoryId = 1 },
                new Category { Id = 6, Name = "weekday_friday", Title = "Friday", ParentCategoryId = 1 },
                new Category { Id = 7, Name = "weekday_saturday", Title = "Saturday", ParentCategoryId = 1 },
                new Category { Id = 8, Name = "weekday_sunday", Title = "Sunday", ParentCategoryId = 1 },
                // Time
                new Category { Id = 9, Name = "time", Title = "Time" },
                new Category { Id = 10, Name = "time_today", Title = "Today", ParentCategoryId = 9 },
                new Category { Id = 11, Name = "time_week", Title = "Week", ParentCategoryId = 9 },
                new Category { Id = 12, Name = "time_month", Title = "Month", ParentCategoryId = 9 },
                new Category { Id = 13, Name = "time_year", Title = "Year", ParentCategoryId = 9 },
                new Category { Id = 14, Name = "time_life", Title = "Life", ParentCategoryId = 9 },
                // Categorie
                new Category { Id = 15, Name = "category", Title = "Category" },
                new Category { Id = 16, Name = "category_quote", Title = "Quote", ParentCategoryId = 15 },
                new Category { Id = 17, Name = "category_shoppinglist", Title = "Shopping List", ParentCategoryId = 15 },
                // Activity
                new Category { Id = 18, Name = "activity", Title = "Activity" },
                new Category { Id = 19, Name = "activity_weighttraining", Title = "Weight training", ParentCategoryId = 18 },
                new Category { Id = 20, Name = "activity_running", Title = "Running", ParentCategoryId = 18 },
                new Category { Id = 21, Name = "activity_dancing", Title = "Dancing", ParentCategoryId = 18 }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, UserId = 1, Name = "happy", Title = "Happy", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Tag { Id = 2, UserId = 2, Name = "fun", Title = "Fun", CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

            modelBuilder.Entity<Note>().HasData(
                // User 1 - 3 Notes
                new Note { Id = 1, UserId = 1, NoteTypeId = 1, Date = DateTime.Now, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Journal
                new Note { Id = 2, UserId = 1, NoteTypeId = 3, Date = DateTime.Now, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Task
                new Note { Id = 3, UserId = 1, NoteTypeId = 8, Date = DateTime.Now, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }, // Weight Measurement
                // User 2 - 1 Note
                new Note { Id = 4, UserId = 2, NoteTypeId = 1, Date = DateTime.Now, CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now } // Journal
            );

            modelBuilder.Entity<NoteValue>().HasData(
                // U1 - N1
                new NoteValue { Id = 1, UserId = 1, NoteId = 1, NoteFieldId = 1, Value = "Test Title", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 2, UserId = 1, NoteId = 1, NoteFieldId = 2, Value = "**Test** Description\n\n# Test Header", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 3, UserId = 1, NoteId = 1, NoteFieldId = 3, Value = "5", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                // U1 - N2
                new NoteValue { Id = 4, UserId = 1, NoteId = 2, NoteFieldId = 4, Value = "Test Title", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 5, UserId = 1, NoteId = 2, NoteFieldId = 5, Value = "**Test** Description\n\n# Test Header", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                // U1 - N3
                new NoteValue { Id = 6, UserId = 1, NoteId = 3, NoteFieldId = 21, Value = "Short test description", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 7, UserId = 1, NoteId = 3, NoteFieldId = 22, Value = "80", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 8, UserId = 1, NoteId = 3, NoteFieldId = 23, Value = "180", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 9, UserId = 1, NoteId = 3, NoteFieldId = 24, Value = "78", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 10, UserId = 1, NoteId = 3, NoteFieldId = 25, Value = "24,69", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 11, UserId = 1, NoteId = 3, NoteFieldId = 26, Value = "13,72", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                // U2 - N1
                new NoteValue { Id = 12, UserId = 2, NoteId = 4, NoteFieldId = 1, Value = "Test Title", CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 13, UserId = 2, NoteId = 4, NoteFieldId = 2, Value = "**Test** Description\n\n# Test Header", CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 14, UserId = 2, NoteId = 4, NoteFieldId = 3, Value = "5", CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );
        }
    }
}
