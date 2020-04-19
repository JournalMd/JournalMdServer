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
                // Journal (No Category - Journal should cover everything in a day)
                new NoteField { Id = 1, NoteTypeId = 1, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 2, NoteTypeId = 1, Order = 2, Name = "description", Title = "Description", Description = "", Required = true, Rules = "", Type = "markdown" },
                new NoteField { Id = 3, NoteTypeId = 1, Order = 3, Name = "mood", Title = "Mood", Description = "", Required = true, Rules = "", Type = "mood" },
                new NoteField { Id = 4, NoteTypeId = 1, Order = 4, Name = "tags", Title = "Tags", Description = "", Required = false, Rules = "", Type = "tags" },
                // Note
                new NoteField { Id = 5, NoteTypeId = 2, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 6, NoteTypeId = 2, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 7, NoteTypeId = 2, Order = 3, Name = "categories", Title = "Categories", Description = "", Required = false, Rules = "", Type = "categories" },
                new NoteField { Id = 8, NoteTypeId = 2, Order = 4, Name = "tags", Title = "Tags", Description = "", Required = false, Rules = "", Type = "tags" },
                // Task
                new NoteField { Id = 9, NoteTypeId = 3, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 10, NoteTypeId = 3, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 11, NoteTypeId = 3, Order = 3, Name = "completed", Title = "Completed", Description = "Is it done?", Required = true, Rules = "", Type = "boolean" },
                new NoteField { Id = 12, NoteTypeId = 3, Order = 4, Name = "due", Title = "Due", Description = "When is it due?", Required = false, Rules = "", Type = "datetime" },
                new NoteField { Id = 13, NoteTypeId = 3, Order = 5, Name = "categories", Title = "Categories", Description = "", Required = false, Rules = "", Type = "categories" },
                new NoteField { Id = 14, NoteTypeId = 3, Order = 6, Name = "tags", Title = "Tags", Description = "", Required = false, Rules = "", Type = "tags" },
                // Goal
                new NoteField { Id = 15, NoteTypeId = 4, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 16, NoteTypeId = 4, Order = 2, Name = "description", Title = "", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 17, NoteTypeId = 4, Order = 3, Name = "achieved", Title = "Completed", Description = "Did you make it?", Required = true, Rules = "", Type = "boolean" },
                new NoteField { Id = 18, NoteTypeId = 4, Order = 4, Name = "due", Title = "Due", Description = "When is it due?", Required = false, Rules = "", Type = "datetime" },
                new NoteField { Id = 19, NoteTypeId = 4, Order = 5, Name = "categories", Title = "Categories", Description = "", Required = false, Rules = "", Type = "categories" },
                new NoteField { Id = 20, NoteTypeId = 4, Order = 6, Name = "tags", Title = "Tags", Description = "", Required = false, Rules = "", Type = "tags" },
                // Activity
                new NoteField { Id = 21, NoteTypeId = 5, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 22, NoteTypeId = 5, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 23, NoteTypeId = 5, Order = 3, Name = "mood", Title = "Mood", Description = "", Required = true, Rules = "", Type = "mood" },
                new NoteField { Id = 24, NoteTypeId = 5, Order = 4, Name = "categories", Title = "Categories", Description = "", Required = false, Rules = "", Type = "categories" },
                new NoteField { Id = 25, NoteTypeId = 5, Order = 5, Name = "tags", Title = "Tags", Description = "", Required = false, Rules = "", Type = "tags" },
                // Habit
                new NoteField { Id = 26, NoteTypeId = 6, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 27, NoteTypeId = 6, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 28, NoteTypeId = 6, Order = 3, Name = "categories", Title = "Categories", Description = "", Required = false, Rules = "", Type = "categories" },
                new NoteField { Id = 29, NoteTypeId = 6, Order = 4, Name = "tags", Title = "Tags", Description = "", Required = false, Rules = "", Type = "tags" },
                // Routine
                new NoteField { Id = 30, NoteTypeId = 7, Order = 1, Name = "title", Title = "Title", Description = "", Required = true, Rules = "", Type = "text" },
                new NoteField { Id = 31, NoteTypeId = 7, Order = 2, Name = "description", Title = "Description", Description = "", Required = false, Rules = "", Type = "markdown" },
                new NoteField { Id = 32, NoteTypeId = 7, Order = 3, Name = "categories", Title = "Categories", Description = "", Required = false, Rules = "", Type = "categories" },
                new NoteField { Id = 33, NoteTypeId = 7, Order = 4, Name = "tags", Title = "Tags", Description = "", Required = false, Rules = "", Type = "tags" },
                // Weightmeasurement (no title)
                new NoteField { Id = 34, NoteTypeId = 8, Order = 1, Name = "shortdescription", Title = "Short Description", Description = "", Required = false, Rules = "", Type = "markdownsingle" },
                new NoteField { Id = 35, NoteTypeId = 8, Order = 2, Name = "weight", Title = "Weight", Description = "Your weight in kg.", Required = true, Rules = "", Type = "number" },
                new NoteField { Id = 36, NoteTypeId = 8, Order = 3, Name = "height", Title = "Height", Description = "Your height in cm.", Required = true, Rules = "default=previous", Type = "number" },
                new NoteField { Id = 37, NoteTypeId = 8, Order = 4, Name = "goalweight", Title = "Goal Weight", Description = "", Required = false, Rules = "default=previous", Type = "number" },
                new NoteField { Id = 38, NoteTypeId = 8, Order = 5, Name = "bodymassindex", Title = "Body-Mass-Index", Description = "", Required = false, Rules = "calculation=bodymassindex", Type = "calculated" },
                new NoteField { Id = 39, NoteTypeId = 8, Order = 6, Name = "ponderalindex", Title = "Ponderal-Index", Description = "", Required = false, Rules = "calculation=ponderalindex", Type = "calculated" },
                // Bodymeasurement (no title)
                new NoteField { Id = 40, NoteTypeId = 9, Order = 1, Name = "shortdescription", Title = "Short Description", Description = "", Required = false, Rules = "", Type = "markdownsingle" },
                new NoteField { Id = 41, NoteTypeId = 9, Order = 2, Name = "chest", Title = "Chest", Description = "", Required = false, Rules = "", Type = "number" }, // Brustumfang
                new NoteField { Id = 42, NoteTypeId = 9, Order = 3, Name = "waist", Title = "Waist", Description = "", Required = true, Rules = "", Type = "number" }, // Taillenumfang
                new NoteField { Id = 43, NoteTypeId = 9, Order = 4, Name = "hips", Title = "Hips", Description = "", Required = true, Rules = "", Type = "number" }, // Hüftumfang
                new NoteField { Id = 44, NoteTypeId = 9, Order = 5, Name = "arm", Title = "Arm", Description = "Larger biceps", Required = false, Rules = "", Type = "number" }, // Oberarm/Biceps
                new NoteField { Id = 45, NoteTypeId = 9, Order = 6, Name = "leg", Title = "Leg", Description = "Larger hamstrings/quadriceps", Required = false, Rules = "", Type = "number" }, // Oberschenkel
                new NoteField { Id = 46, NoteTypeId = 9, Order = 7, Name = "calf", Title = "Calf", Description = "Larger calf", Required = false, Rules = "", Type = "number" },// Waden
                new NoteField { Id = 47, NoteTypeId = 9, Order = 8, Name = "bodyfatmass", Title = "Body-Fat-Mass", Description = "", Required = false, Rules = "", Type = "number" }, // Körperfettanteil
                new NoteField { Id = 48, NoteTypeId = 9, Order = 9, Name = "bodyfatpercentage", Title = "Body-Fat-Percentage", Description = "", Required = false, Rules = "", Type = "number" }, // Body Fat %
                new NoteField { Id = 49, NoteTypeId = 9, Order = 10, Name = "totalbodywater", Title = "Total-Body-Water", Description = "", Required = false, Rules = "", Type = "number" }, // Wasseranteil
                new NoteField { Id = 50, NoteTypeId = 9, Order = 11, Name = "musclemass", Title = "Muscle-Mass", Description = "", Required = false, Rules = "", Type = "number" }, // Muskelanteil
                new NoteField { Id = 51, NoteTypeId = 9, Order = 12, Name = "waisttohipratio", Title = "Waist-To-Hip-Ratio", Description = "", Required = false, Rules = "calculation=waisttohipratio", Type = "calculated" }
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
                new Note { Id = 1, UserId = 1, NoteTypeId = 1, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Note { Id = 2, UserId = 1, NoteTypeId = 1, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Note { Id = 3, UserId = 1, NoteTypeId = 1, CreatedById = 1, UpdatedById = 1, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },

                new Note { Id = 4, UserId = 2, NoteTypeId = 1, CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new Note { Id = 5, UserId = 2, NoteTypeId = 1, CreatedById = 2, UpdatedById = 2, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );
        }
    }
}
