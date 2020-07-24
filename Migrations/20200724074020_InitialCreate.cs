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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        .Annotation("Sqlite:Autoincrement", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                values: new object[,]
                {
                    { 1L, "weekday", null, "Weekday" },
                    { 9L, "time", null, "Time" },
                    { 15L, "category", null, "Category" },
                    { 18L, "activity", null, "Activity" }
                });

            migrationBuilder.InsertData(
                table: "NoteTypes",
                columns: new[] { "Id", "Description", "Name", "NoteDescriptionShort", "Order", "Title" },
                values: new object[,]
                {
                    { 1L, "Summarize your day or write down your thoughts.", "journal", false, 1, "Journal" },
                    { 2L, "Write a simple note.", "note", false, 2, "Note" },
                    { 3L, "A task you must complete.", "task", false, 3, "Task" },
                    { 4L, "A goal you want to achieve.", "goal", false, 5, "Goal" },
                    { 5L, "Something you've done.", "activity", false, 6, "Activity" },
                    { 6L, "Record your habits.", "habit", false, 7, "Habit" },
                    { 7L, "Write down what you want to do every day.", "routine", false, 8, "Routine" },
                    { 8L, "Track your weight.", "weightmeasurement", true, 9, "Weight Measurement" },
                    { 9L, "Track your body measurements.", "bodymeasurement", true, 10, "Body Measurement" },
                    { 10L, "List of tasks.", "project", false, 4, "Project" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "PasswordHash", "PasswordSalt", "Token", "Username" },
                values: new object[,]
                {
                    { 1L, null, "Max", null, "Power", new byte[] { 0, 200, 2, 140, 63, 180, 122, 28, 141, 217, 112, 188, 249, 89, 179, 218, 16, 251, 218, 118, 195, 169, 48, 173, 9, 194, 108, 13, 29, 60, 52, 169, 123, 121, 114, 237, 58, 202, 26, 28, 81, 52, 111, 236, 207, 218, 207, 225, 116, 175, 119, 89, 67, 84, 34, 121, 178, 123, 0, 49, 108, 150, 121, 4 }, new byte[] { 31, 49, 34, 125, 247, 129, 177, 217, 143, 30, 64, 133, 77, 6, 66, 65, 214, 48, 143, 20, 191, 210, 124, 161, 133, 140, 83, 84, 47, 49, 89, 153, 245, 13, 76, 187, 132, 64, 110, 114, 114, 106, 26, 153, 138, 138, 184, 210, 216, 2, 223, 85, 213, 182, 201, 124, 227, 39, 239, 65, 23, 229, 28, 109, 147, 66, 68, 236, 142, 103, 208, 24, 49, 226, 220, 46, 143, 63, 178, 154, 194, 91, 112, 47, 178, 192, 106, 68, 16, 155, 72, 62, 96, 205, 142, 18, 17, 94, 145, 229, 174, 208, 246, 207, 63, 81, 16, 99, 227, 171, 87, 54, 178, 234, 33, 194, 35, 157, 173, 163, 96, 195, 47, 249, 220, 77, 147, 136 }, null, "1" },
                    { 2L, null, null, null, null, new byte[] { 0, 200, 2, 140, 63, 180, 122, 28, 141, 217, 112, 188, 249, 89, 179, 218, 16, 251, 218, 118, 195, 169, 48, 173, 9, 194, 108, 13, 29, 60, 52, 169, 123, 121, 114, 237, 58, 202, 26, 28, 81, 52, 111, 236, 207, 218, 207, 225, 116, 175, 119, 89, 67, 84, 34, 121, 178, 123, 0, 49, 108, 150, 121, 4 }, new byte[] { 31, 49, 34, 125, 247, 129, 177, 217, 143, 30, 64, 133, 77, 6, 66, 65, 214, 48, 143, 20, 191, 210, 124, 161, 133, 140, 83, 84, 47, 49, 89, 153, 245, 13, 76, 187, 132, 64, 110, 114, 114, 106, 26, 153, 138, 138, 184, 210, 216, 2, 223, 85, 213, 182, 201, 124, 227, 39, 239, 65, 23, 229, 28, 109, 147, 66, 68, 236, 142, 103, 208, 24, 49, 226, 220, 46, 143, 63, 178, 154, 194, 91, 112, 47, 178, 192, 106, 68, 16, 155, 72, 62, 96, 205, 142, 18, 17, 94, 145, 229, 174, 208, 246, 207, 63, 81, 16, 99, 227, 171, 87, 54, 178, 234, 33, 194, 35, 157, 173, 163, 96, 195, 47, 249, 220, 77, 147, 136 }, null, "2" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId", "Title" },
                values: new object[,]
                {
                    { 2L, "weekday_monday", 1L, "Monday" },
                    { 21L, "activity_dancing", 18L, "Dancing" },
                    { 20L, "activity_running", 18L, "Running" },
                    { 17L, "category_shoppinglist", 15L, "Shopping List" },
                    { 16L, "category_quote", 15L, "Quote" },
                    { 14L, "time_life", 9L, "Life" },
                    { 13L, "time_year", 9L, "Year" },
                    { 12L, "time_month", 9L, "Month" },
                    { 19L, "activity_weighttraining", 18L, "Weight training" },
                    { 10L, "time_today", 9L, "Today" },
                    { 8L, "weekday_sunday", 1L, "Sunday" },
                    { 7L, "weekday_saturday", 1L, "Saturday" },
                    { 6L, "weekday_friday", 1L, "Friday" },
                    { 5L, "weekday_thursday", 1L, "Thursday" },
                    { 4L, "weekday_wednesday", 1L, "Wednesday" },
                    { 3L, "weekday_tuesday", 1L, "Tuesday" },
                    { 11L, "time_week", 9L, "Week" }
                });

            migrationBuilder.InsertData(
                table: "NoteFields",
                columns: new[] { "Id", "Description", "Name", "NoteTypeId", "Order", "Required", "Rules", "Title", "Type" },
                values: new object[,]
                {
                    { 16L, "Hamstrings/quadriceps", "legr", 9L, 5, false, "", "Leg Right", "number" },
                    { 17L, "Calf", "calfl", 9L, 6, false, "", "Calf Left", "number" },
                    { 18L, "Calf", "calfr", 9L, 6, false, "", "Calf Right", "number" },
                    { 19L, "", "bodyfatmass", 9L, 7, false, "", "Body-Fat-Mass", "number" },
                    { 20L, "", "bodyfatpercentage", 9L, 8, false, "", "Body-Fat-Percentage", "number" },
                    { 24L, "Is it done?", "completed", 10L, 1, false, "calculation=completed", "Completed", "calculated" },
                    { 22L, "", "musclemass", 9L, 10, false, "", "Muscle-Mass", "number" },
                    { 23L, "", "waisttohipratio", 9L, 11, false, "calculation=waisttohipratio", "Waist-To-Hip-Ratio", "calculated" },
                    { 25L, "When is it due?", "due", 10L, 2, false, "", "Due", "datetime" },
                    { 15L, "Hamstrings/quadriceps", "legl", 9L, 5, false, "", "Leg Left", "number" },
                    { 21L, "", "totalbodywater", 9L, 9, false, "", "Total-Body-Water", "number" },
                    { 14L, "Biceps", "armr", 9L, 4, false, "", "Arm Right", "number" },
                    { 7L, "", "goalweight", 8L, 3, false, "default=previous", "Goal Weight", "number" },
                    { 12L, "", "chest", 9L, 1, false, "", "Chest", "number" },
                    { 1L, "Is it done?", "completed", 3L, 1, false, "", "Completed", "boolean" },
                    { 2L, "When is it due?", "due", 3L, 2, false, "", "Due", "datetime" },
                    { 3L, "Did you make it?", "achieved", 4L, 1, false, "", "Completed", "boolean" },
                    { 13L, "Biceps", "arml", 9L, 4, false, "", "Arm Left", "number" },
                    { 5L, "Your weight in kg.", "weight", 8L, 1, true, "", "Weight", "number" },
                    { 4L, "When is it due?", "due", 4L, 2, false, "", "Due", "datetime" },
                    { 8L, "", "bodymassindex", 8L, 4, false, "calculation=bodymassindex", "Body-Mass-Index", "calculated" },
                    { 9L, "", "ponderalindex", 8L, 5, false, "calculation=ponderalindex", "Ponderal-Index", "calculated" },
                    { 10L, "For Waist-To-Hip Ratio.", "waist", 9L, 2, true, "", "Waist", "number" },
                    { 11L, "For Waist-To-Hip Ratio.", "hips", 9L, 3, true, "", "Hips", "number" },
                    { 6L, "Your height in cm.", "height", 8L, 2, true, "default=previous", "Height", "number" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Date", "Description", "Mood", "NoteTypeId", "Title", "UpdatedAt", "UpdatedById", "UserId" },
                values: new object[,]
                {
                    { 4L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7411), 2L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7410), @"**Test** Description

                # Test Header", 3, 1L, "Test Journal", new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7412), 2L, 2L },
                    { 1L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7336), 1L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7077), @"**Test** Description

                # Test Header", 5, 1L, "Test Journal", new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7340), 1L, 1L },
                    { 2L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7404), 1L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7396), "Test", 1, 3L, "Test Task", new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7405), 1L, 1L },
                    { 3L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7408), 1L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7407), "Test", 3, 8L, "Test Weight Measurement", new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(7409), 1L, 1L }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Name", "Title", "UpdatedAt", "UpdatedById", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 7, 24, 7, 40, 19, 754, DateTimeKind.Local).AddTicks(6305), 1L, "happy", "Happy", new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(4663), 1L, 1L },
                    { 2L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(5014), 2L, "fun", "Fun", new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(5029), 2L, 2L }
                });

            migrationBuilder.InsertData(
                table: "NoteValue",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "NoteFieldId", "NoteId", "UpdatedAt", "UpdatedById", "UserId", "Value" },
                values: new object[,]
                {
                    { 1L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9021), 1L, 1L, 2L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9028), 1L, 1L, "false" },
                    { 2L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9122), 1L, 2L, 2L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9123), 1L, 1L, "2022-10-01" },
                    { 4L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9125), 1L, 5L, 3L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9126), 1L, 1L, "80" },
                    { 5L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9128), 1L, 6L, 3L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9129), 1L, 1L, "180" },
                    { 6L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9131), 1L, 7L, 3L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9131), 1L, 1L, "78" },
                    { 7L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9134), 1L, 8L, 3L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9134), 1L, 1L, "24,69" },
                    { 8L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9136), 1L, 9L, 3L, new DateTime(2020, 7, 24, 7, 40, 19, 755, DateTimeKind.Local).AddTicks(9137), 1L, 1L, "13,72" }
                });

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
