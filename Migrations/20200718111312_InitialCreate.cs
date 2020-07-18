using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalMdServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        //.Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    ParentCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NoteTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        //.Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Order = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    NoteDescriptionShort = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        //.Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteFields",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        //.Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Order = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    Rules = table.Column<string>(nullable: true),
                    NoteTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteFields_NoteTypes_NoteTypeId",
                        column: x => x.NoteTypeId,
                        principalTable: "NoteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        //.Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<long>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Mood = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NoteTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_NoteTypes_NoteTypeId",
                        column: x => x.NoteTypeId,
                        principalTable: "NoteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        //.Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<long>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteCategory",
                columns: table => new
                {
                    NoteId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteCategory", x => new { x.NoteId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_NoteCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteCategory_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteValue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                        //.Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<long>(nullable: false),
                    UpdatedById = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    NoteId = table.Column<long>(nullable: false),
                    NoteFieldId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteValue_NoteFields_NoteFieldId",
                        column: x => x.NoteFieldId,
                        principalTable: "NoteFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteValue_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteValue_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteTag",
                columns: table => new
                {
                    NoteId = table.Column<long>(nullable: false),
                    TagId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTag", x => new { x.NoteId, x.TagId });
                    table.ForeignKey(
                        name: "FK_NoteTag_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 1L, "weekday", null, "Weekday" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 9L, "time", null, "Time" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 15L, "category", null, "Category" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 18L, "activity", null, "Activity" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 1L, "Summarize your day or write down your thoughts.", "journal", false, 1, "Journal" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 2L, "Write a simple note.", "note", false, 2, "Note" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 3L, "A task you must complete.", "task", false, 3, "Task" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 4L, "A goal you want to achieve.", "goal", false, 5, "Goal" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 5L, "Something you've done.", "activity", false, 6, "Activity" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 6L, "Record your habits.", "habit", false, 7, "Habit" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 7L, "Write down what you want to do every day.", "routine", false, 8, "Routine" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 8L, "Track your weight.", "weightmeasurement", true, 9, "Weight Measurement" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 9L, "Track your body measurements.", "bodymeasurement", true, 10, "Body Measurement" });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[] { 10L, "List of tasks.", "project", false, 4, "Project" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordSalt", "Token", "Username" },
                values: new object[] { 1L, null, "Max", null, "Power", new byte[] { 223, 233, 254, 207, 22, 246, 226, 106, 163, 18, 242, 106, 130, 134, 221, 53, 31, 98, 13, 6, 203, 189, 175, 218, 238, 202, 156, 193, 14, 124, 139, 158, 109, 193, 205, 90, 50, 102, 153, 189, 14, 68, 92, 87, 131, 202, 159, 116, 187, 111, 170, 59, 68, 135, 16, 176, 231, 189, 4, 188, 33, 207, 186, 244 }, new byte[] { 77, 107, 53, 3, 195, 35, 231, 43, 121, 10, 66, 220, 100, 241, 191, 154, 8, 102, 225, 67, 218, 59, 248, 243, 81, 154, 96, 122, 133, 97, 202, 59, 236, 102, 162, 191, 121, 50, 146, 5, 88, 232, 131, 188, 4, 215, 180, 216, 183, 93, 151, 35, 205, 16, 136, 137, 175, 42, 127, 227, 150, 122, 134, 53, 119, 79, 108, 171, 32, 29, 184, 175, 189, 117, 67, 255, 149, 1, 94, 113, 240, 184, 92, 172, 216, 100, 157, 11, 214, 52, 145, 116, 19, 102, 145, 137, 86, 54, 105, 111, 78, 81, 223, 149, 222, 82, 43, 81, 249, 172, 86, 83, 146, 171, 123, 154, 113, 11, 130, 88, 240, 106, 70, 167, 185, 81, 179, 246 }, null, "1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordSalt", "Token", "Username" },
                values: new object[] { 2L, null, null, null, null, new byte[] { 223, 233, 254, 207, 22, 246, 226, 106, 163, 18, 242, 106, 130, 134, 221, 53, 31, 98, 13, 6, 203, 189, 175, 218, 238, 202, 156, 193, 14, 124, 139, 158, 109, 193, 205, 90, 50, 102, 153, 189, 14, 68, 92, 87, 131, 202, 159, 116, 187, 111, 170, 59, 68, 135, 16, 176, 231, 189, 4, 188, 33, 207, 186, 244 }, new byte[] { 77, 107, 53, 3, 195, 35, 231, 43, 121, 10, 66, 220, 100, 241, 191, 154, 8, 102, 225, 67, 218, 59, 248, 243, 81, 154, 96, 122, 133, 97, 202, 59, 236, 102, 162, 191, 121, 50, 146, 5, 88, 232, 131, 188, 4, 215, 180, 216, 183, 93, 151, 35, 205, 16, 136, 137, 175, 42, 127, 227, 150, 122, 134, 53, 119, 79, 108, 171, 32, 29, 184, 175, 189, 117, 67, 255, 149, 1, 94, 113, 240, 184, 92, 172, 216, 100, 157, 11, 214, 52, 145, 116, 19, 102, 145, 137, 86, 54, 105, 111, 78, 81, 223, 149, 222, 82, 43, 81, 249, 172, 86, 83, 146, 171, 123, 154, 113, 11, 130, 88, 240, 106, 70, 167, 185, 81, 179, 246 }, null, "2" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 2L, "weekday_monday", 1L, "Monday" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 21L, "activity_dancing", 18L, "Dancing" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 20L, "activity_running", 18L, "Running" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 17L, "category_shoppinglist", 15L, "Shopping List" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 16L, "category_quote", 15L, "Quote" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 14L, "time_life", 9L, "Life" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 13L, "time_year", 9L, "Year" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 12L, "time_month", 9L, "Month" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 19L, "activity_weighttraining", 18L, "Weight training" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 10L, "time_today", 9L, "Today" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 8L, "weekday_sunday", 1L, "Sunday" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 7L, "weekday_saturday", 1L, "Saturday" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 6L, "weekday_friday", 1L, "Friday" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 5L, "weekday_thursday", 1L, "Thursday" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 4L, "weekday_wednesday", 1L, "Wednesday" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 3L, "weekday_tuesday", 1L, "Tuesday" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[] { 11L, "time_week", 9L, "Week" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 16L, "Hamstrings/quadriceps", "legr", 9L, 5, false, "", "Leg Right", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 17L, "Calf", "calfl", 9L, 6, false, "", "Calf Left", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 18L, "Calf", "calfr", 9L, 6, false, "", "Calf Right", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 19L, "", "bodyfatmass", 9L, 7, false, "", "Body-Fat-Mass", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 20L, "", "bodyfatpercentage", 9L, 8, false, "", "Body-Fat-Percentage", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 24L, "Is it done?", "completed", 10L, 1, false, "calculation=completed", "Completed", "calculated" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 22L, "", "musclemass", 9L, 10, false, "", "Muscle-Mass", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 23L, "", "waisttohipratio", 9L, 11, false, "calculation=waisttohipratio", "Waist-To-Hip-Ratio", "calculated" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 25L, "When is it due?", "due", 10L, 2, false, "", "Due", "datetime" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 15L, "Hamstrings/quadriceps", "legl", 9L, 5, false, "", "Leg Left", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 21L, "", "totalbodywater", 9L, 9, false, "", "Total-Body-Water", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 14L, "Biceps", "armr", 9L, 4, false, "", "Arm Right", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 7L, "", "goalweight", 8L, 3, false, "default=previous", "Goal Weight", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 12L, "", "chest", 9L, 1, false, "", "Chest", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 1L, "Is it done?", "completed", 3L, 1, false, "", "Completed", "boolean" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 2L, "When is it due?", "due", 3L, 2, false, "", "Due", "datetime" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 3L, "Did you make it?", "achieved", 4L, 1, false, "", "Completed", "boolean" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 13L, "Biceps", "arml", 9L, 4, false, "", "Arm Left", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 5L, "Your weight in kg.", "weight", 8L, 1, true, "", "Weight", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 4L, "When is it due?", "due", 4L, 2, false, "", "Due", "datetime" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 8L, "", "bodymassindex", 8L, 4, false, "calculation=bodymassindex", "Body-Mass-Index", "calculated" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 9L, "", "ponderalindex", 8L, 5, false, "calculation=ponderalindex", "Ponderal-Index", "calculated" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 10L, "For Waist-To-Hip Ratio.", "waist", 9L, 2, true, "", "Waist", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 11L, "For Waist-To-Hip Ratio.", "hips", 9L, 3, true, "", "Hips", "number" });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[] { 6L, "Your height in cm.", "height", 8L, 2, true, "default=previous", "Height", "number" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Date", "Description", "Mood", "NoteTypeId", "Title", "UpdatedAt", "UpdatedById", "UserId" },
                values: new object[] { 4L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3323), 2L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3319), @"**Test** Description

# Test Header", 3, 1L, "Test Journal", new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3326), 2L, 2L });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Date", "Description", "Mood", "NoteTypeId", "Title", "UpdatedAt", "UpdatedById", "UserId" },
                values: new object[] { 1L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3190), 1L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(2557), @"**Test** Description

# Test Header", 5, 1L, "Test Journal", new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3205), 1L, 1L });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Date", "Description", "Mood", "NoteTypeId", "Title", "UpdatedAt", "UpdatedById", "UserId" },
                values: new object[] { 2L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3296), 1L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3284), "Test", 1, 3L, "Test Task", new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3300), 1L, 1L });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Date", "Description", "Mood", "NoteTypeId", "Title", "UpdatedAt", "UpdatedById", "UserId" },
                values: new object[] { 3L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3310), 1L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3306), "Test", 3, 8L, "Test Weight Measurement", new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(3314), 1L, 1L });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Name", "Title", "UpdatedAt", "UpdatedById", "UserId" },
                values: new object[] { 1L, new DateTime(2020, 7, 18, 13, 13, 12, 200, DateTimeKind.Local).AddTicks(1227), 1L, "happy", "Happy", new DateTime(2020, 7, 18, 13, 13, 12, 202, DateTimeKind.Local).AddTicks(8642), 1L, 1L });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Name", "Title", "UpdatedAt", "UpdatedById", "UserId" },
                values: new object[] { 2L, new DateTime(2020, 7, 18, 13, 13, 12, 202, DateTimeKind.Local).AddTicks(9376), 2L, "fun", "Fun", new DateTime(2020, 7, 18, 13, 13, 12, 202, DateTimeKind.Local).AddTicks(9399), 2L, 2L });

            migrationBuilder.InsertData(
                table: "NoteValue",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "NoteFieldId", "NoteId", "UpdatedAt", "UpdatedById", "UserId", "Value" },
                values: new object[] { 1L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5691), 1L, 1L, 2L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5710), 1L, 1L, "false" });

            migrationBuilder.InsertData(
                table: "NoteValue",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "NoteFieldId", "NoteId", "UpdatedAt", "UpdatedById", "UserId", "Value" },
                values: new object[] { 2L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5760), 1L, 2L, 2L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5765), 1L, 1L, "2022-10-01" });

            migrationBuilder.InsertData(
                table: "NoteValue",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "NoteFieldId", "NoteId", "UpdatedAt", "UpdatedById", "UserId", "Value" },
                values: new object[] { 4L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5770), 1L, 5L, 3L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5774), 1L, 1L, "80" });

            migrationBuilder.InsertData(
                table: "NoteValue",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "NoteFieldId", "NoteId", "UpdatedAt", "UpdatedById", "UserId", "Value" },
                values: new object[] { 5L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5779), 1L, 6L, 3L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5783), 1L, 1L, "180" });

            migrationBuilder.InsertData(
                table: "NoteValue",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "NoteFieldId", "NoteId", "UpdatedAt", "UpdatedById", "UserId", "Value" },
                values: new object[] { 6L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5787), 1L, 7L, 3L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5791), 1L, 1L, "78" });

            migrationBuilder.InsertData(
                table: "NoteValue",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "NoteFieldId", "NoteId", "UpdatedAt", "UpdatedById", "UserId", "Value" },
                values: new object[] { 7L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5796), 1L, 8L, 3L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5800), 1L, 1L, "24,69" });

            migrationBuilder.InsertData(
                table: "NoteValue",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "NoteFieldId", "NoteId", "UpdatedAt", "UpdatedById", "UserId", "Value" },
                values: new object[] { 8L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5805), 1L, 9L, 3L, new DateTime(2020, 7, 18, 13, 13, 12, 203, DateTimeKind.Local).AddTicks(5809), 1L, 1L, "13,72" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteCategory_CategoryId",
                table: "NoteCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteFields_NoteTypeId",
                table: "NoteFields",
                column: "NoteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteTypeId",
                table: "Notes",
                column: "NoteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteTag_TagId",
                table: "NoteTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteValue_NoteFieldId",
                table: "NoteValue",
                column: "NoteFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteValue_NoteId",
                table: "NoteValue",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteValue_UserId",
                table: "NoteValue",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserId",
                table: "Tags",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteCategory");

            migrationBuilder.DropTable(
                name: "NoteTag");

            migrationBuilder.DropTable(
                name: "NoteValue");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "NoteFields");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "NoteTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
