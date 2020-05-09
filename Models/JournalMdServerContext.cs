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
                new NoteType { Id = 1, Order = 1, Name = "journal", Title = "Journal", Description = "Summarize your day or write down your thoughts.", NoteDescriptionShort = false },
                new NoteType { Id = 2, Order = 2, Name = "note", Title = "Note", Description = "Write a simple note.", NoteDescriptionShort = false },
                new NoteType { Id = 3, Order = 3, Name = "task", Title = "Task", Description = "A task you must complete.", NoteDescriptionShort = false },
                new NoteType { Id = 4, Order = 5, Name = "goal", Title = "Goal", Description = "A goal you want to achieve.", NoteDescriptionShort = false },
                new NoteType { Id = 5, Order = 6, Name = "activity", Title = "Activity", Description = "Something you've done.", NoteDescriptionShort = false },
                new NoteType { Id = 6, Order = 7, Name = "habit", Title = "Habit", Description = "Record your habits.", NoteDescriptionShort = false },
                new NoteType { Id = 7, Order = 8, Name = "routine", Title = "Routine", Description = "Write down what you want to do every day.", NoteDescriptionShort = false },
                new NoteType { Id = 8, Order = 9, Name = "weightmeasurement", Title = "Weight Measurement", Description = "Track your weight.", NoteDescriptionShort = true },
                new NoteType { Id = 9, Order = 10, Name = "bodymeasurement", Title = "Body Measurement", Description = "Track your body measurements.", NoteDescriptionShort = true },
                new NoteType { Id = 10, Order = 4, Name = "project", Title = "Project", Description = "List of tasks.", NoteDescriptionShort = false }
            );

            modelBuilder.Entity<NoteField>().HasData(
                // Journal
                // Note
                // Task
                new NoteField { Id = 1, NoteTypeId = 3, Order = 1, Name = "completed", Title = "Completed", Description = "Is it done?", Required = false /* "" = false */, Rules = "", Type = "boolean" },
                new NoteField { Id = 2, NoteTypeId = 3, Order = 2, Name = "due", Title = "Due", Description = "When is it due?", Required = false, Rules = "", Type = "datetime" },
                // Goal
                new NoteField { Id = 3, NoteTypeId = 4, Order = 1, Name = "achieved", Title = "Completed", Description = "Did you make it?", Required = false /* "" = false */, Rules = "", Type = "boolean" },
                new NoteField { Id = 4, NoteTypeId = 4, Order = 2, Name = "due", Title = "Due", Description = "When is it due?", Required = false, Rules = "", Type = "datetime" },
                // Activity
                // Habit
                // Routine
                // Weightmeasurement
                new NoteField { Id = 5, NoteTypeId = 8, Order = 1, Name = "weight", Title = "Weight", Description = "Your weight in kg.", Required = true, Rules = "", Type = "number" },
                new NoteField { Id = 6, NoteTypeId = 8, Order = 2, Name = "height", Title = "Height", Description = "Your height in cm.", Required = true, Rules = "default=previous", Type = "number" },
                new NoteField { Id = 7, NoteTypeId = 8, Order = 3, Name = "goalweight", Title = "Goal Weight", Description = "", Required = false, Rules = "default=previous", Type = "number" },
                new NoteField { Id = 8, NoteTypeId = 8, Order = 4, Name = "bodymassindex", Title = "Body-Mass-Index", Description = "", Required = false, Rules = "calculation=bodymassindex", Type = "calculated" },
                new NoteField { Id = 9, NoteTypeId = 8, Order = 5, Name = "ponderalindex", Title = "Ponderal-Index", Description = "", Required = false, Rules = "calculation=ponderalindex", Type = "calculated" },
                // Bodymeasurement
                new NoteField { Id = 10, NoteTypeId = 9, Order = 1, Name = "chest", Title = "Chest", Description = "", Required = false, Rules = "", Type = "number" }, // Brustumfang
                new NoteField { Id = 11, NoteTypeId = 9, Order = 2, Name = "waist", Title = "Waist", Description = "", Required = true, Rules = "", Type = "number" }, // Taillenumfang
                new NoteField { Id = 12, NoteTypeId = 9, Order = 3, Name = "hips", Title = "Hips", Description = "", Required = true, Rules = "", Type = "number" }, // Hüftumfang
                new NoteField { Id = 13, NoteTypeId = 9, Order = 4, Name = "arm", Title = "Arm", Description = "Larger biceps", Required = false, Rules = "", Type = "number" }, // Oberarm/Biceps
                new NoteField { Id = 14, NoteTypeId = 9, Order = 5, Name = "leg", Title = "Leg", Description = "Larger hamstrings/quadriceps", Required = false, Rules = "", Type = "number" }, // Oberschenkel
                new NoteField { Id = 15, NoteTypeId = 9, Order = 6, Name = "calf", Title = "Calf", Description = "Larger calf", Required = false, Rules = "", Type = "number" },// Waden
                new NoteField { Id = 16, NoteTypeId = 9, Order = 7, Name = "bodyfatmass", Title = "Body-Fat-Mass", Description = "", Required = false, Rules = "", Type = "number" }, // Körperfettanteil
                new NoteField { Id = 17, NoteTypeId = 9, Order = 8, Name = "bodyfatpercentage", Title = "Body-Fat-Percentage", Description = "", Required = false, Rules = "", Type = "number" }, // Body Fat %
                new NoteField { Id = 18, NoteTypeId = 9, Order = 9, Name = "totalbodywater", Title = "Total-Body-Water", Description = "", Required = false, Rules = "", Type = "number" }, // Wasseranteil
                new NoteField { Id = 19, NoteTypeId = 9, Order = 10, Name = "musclemass", Title = "Muscle-Mass", Description = "", Required = false, Rules = "", Type = "number" }, // Muskelanteil
                new NoteField { Id = 20, NoteTypeId = 9, Order = 11, Name = "waisttohipratio", Title = "Waist-To-Hip-Ratio", Description = "", Required = false, Rules = "calculation=waisttohipratio", Type = "calculated" },
                // Project
                new NoteField { Id = 21, NoteTypeId = 10, Order = 1, Name = "completed", Title = "Completed", Description = "Is it done?", Required = false, Rules = "calculation=completed", Type = "calculated" },
                new NoteField { Id = 22, NoteTypeId = 10, Order = 2, Name = "due", Title = "Due", Description = "When is it due?", Required = false, Rules = "", Type = "datetime" }
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
                new Note { Id = 1, UserId = 1, NoteTypeId = 1, Title = "Test Journal", Description = "**Test** Description\n\n# Test Header", Mood = 5, Date = DateTime.Now, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Note { Id = 2, UserId = 1, NoteTypeId = 3, Title = "Test Task", Description = "Test", Mood = 1, Date = DateTime.Now, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Note { Id = 3, UserId = 1, NoteTypeId = 8, Title = "Test Weight Measurement", Description = "Test", Mood = 3, Date = DateTime.Now, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                // User 2 - 1 Note
                new Note { Id = 4, UserId = 2, NoteTypeId = 1, Title = "Test Journal", Description = "**Test** Description\n\n# Test Header", Mood = 3, Date = DateTime.Now, CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

            modelBuilder.Entity<NoteValue>().HasData(
                // U1 - N1
                // U1 - N2
                new NoteValue { Id = 1, UserId = 1, NoteId = 2, NoteFieldId = 1, Value = "false", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 2, UserId = 1, NoteId = 2, NoteFieldId = 2, Value = "2022-10-01", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                // U1 - N3
                new NoteValue { Id = 4, UserId = 1, NoteId = 3, NoteFieldId = 5, Value = "80", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 5, UserId = 1, NoteId = 3, NoteFieldId = 6, Value = "180", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 6, UserId = 1, NoteId = 3, NoteFieldId = 7, Value = "78", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 7, UserId = 1, NoteId = 3, NoteFieldId = 8, Value = "24,69", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new NoteValue { Id = 8, UserId = 1, NoteId = 3, NoteFieldId = 9, Value = "13,72", CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
                // U2 - N1
            );
        }
    }
}
