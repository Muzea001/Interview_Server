using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Mobile = table.Column<string>(type: "text", nullable: false),
                    LogbookId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logbooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logbooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInterviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    InterviewId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    InterviewTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInterviews_Interviews_InterviewId",
                        column: x => x.InterviewId,
                        principalTable: "Interviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInterviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogbookId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    InterviewId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Interviews_InterviewId",
                        column: x => x.InterviewId,
                        principalTable: "Interviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_Logbooks_LogbookId",
                        column: x => x.LogbookId,
                        principalTable: "Logbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    UserInterviewId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_UserInterviews_UserInterviewId",
                        column: x => x.UserInterviewId,
                        principalTable: "UserInterviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interviews",
                columns: new[] { "Id", "Address", "CompanyName", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Kongens gate 6, Oslo", "PayEx", "Technical interview after a short speedinterview", "Technical Interview" },
                    { 2, "Idrettsveien 8, Ski", "Nordre Follo Kommune", "Bli kjent intervju", "Førstegangsintervju" },
                    { 3, "Helsingborgveien 5, Bergen", "TechCorp Solutions", "A technical interview for a software engineer position", "Software Engineer Interview" },
                    { 4, "Bergen Street 12, Bergen", "GlobalTech Innovations", "Interview for the position of data analyst", "Data Analyst Interview" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LogbookId", "Mobile", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "ali@example.com", 1, "1234", "AQAAAAIAAYagAAAAEADDptPPPl5spwN2AG6tb6pbFYAnl7FMzZomXkGN7/GSnPzVcJEcudxHu+JlrVZWyw==", "Ali Khan" },
                    { 2, "muaath@example.com", 2, "1881", "AQAAAAIAAYagAAAAEOzNW8gNOnm98KwZ3X3b23aNBt4onAJQSoFFT/8F1d6RFNXUqej8KdTvnW4XKJ2Bpg==", "Muaath Zerouga" },
                    { 3, "john@example.com", 3, "123", "AQAAAAIAAYagAAAAEDT6NNeVR5GNt8Xv+LDnUU30Wo2eNyZlm1egfjsBNefmt4HeLU4UymMuddAjTe5LoQ==", "John Ferdie" },
                    { 4, "magnus@example.com", 4, "786", "AQAAAAIAAYagAAAAEBarQP3sSpdCAgyxjqhrsj2ilkfk89TzINMx26o+NvNRt4k7NDbfuAecDHWQCv37+w==", "Magnus Brandsegg" },
                    { 5, "sophia@example.com", 5, "2250", "AQAAAAIAAYagAAAAEKqXvqsoUxjq9YIwjlBN+XFEbcA0KWEGgGLQ6wjRIk6uUNzOikrpQdanX/dEB0nieA==", "Sophia Miller" },
                    { 6, "david@example.com", 6, "4332", "AQAAAAIAAYagAAAAEIpSBo40iZU34C0+emVshGPRubQnv+yM86fhASnKNJDTmasrDA2Wm1X22Uj1FRq0SA==", "David Johnson" }
                });

            migrationBuilder.InsertData(
                table: "Logbooks",
                columns: new[] { "Id", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Ali's Logbook", 1 },
                    { 2, "Muaath's Logbook", 2 },
                    { 3, "John's Logbook", 3 },
                    { 4, "Magnus's Logbook", 4 },
                    { 5, "Sophia's Logbook", 5 },
                    { 6, "David's Logbook", 6 }
                });

            migrationBuilder.InsertData(
                table: "UserInterviews",
                columns: new[] { "Id", "DurationInMinutes", "InterviewId", "InterviewTime", "Role", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 120, 1, new DateTime(2024, 11, 11, 14, 30, 0, 0, DateTimeKind.Utc), "Interviewee", "Scheduled", 1 },
                    { 2, 90, 3, new DateTime(2024, 11, 15, 10, 0, 0, 0, DateTimeKind.Utc), "Interviewee", "Scheduled", 2 },
                    { 3, 60, 4, new DateTime(2024, 11, 16, 11, 15, 0, 0, DateTimeKind.Utc), "Interviewee", "Scheduled", 3 },
                    { 4, 45, 2, new DateTime(2024, 11, 18, 15, 45, 0, 0, DateTimeKind.Utc), "Interviewee", "Scheduled", 4 }
                });

            migrationBuilder.InsertData(
                table: "Logs",
                columns: new[] { "Id", "Content", "InterviewId", "LogbookId", "Title" },
                values: new object[,]
                {
                    { 1, "Learned about interview preparation and key technical questions", 1, 1, "Log 1" },
                    { 2, "Studied Python and algorithms for the next interview", 1, 1, "Log 2" },
                    { 3, "Discovered effective ways to answer behavioral questions", 3, 2, "Log 1" },
                    { 4, "Reviewed data analysis tools like Excel, Tableau, and Power BI", 3, 2, "Log 2" },
                    { 5, "Prepared for coding tests and problem-solving strategies", 4, 3, "Log 1" },
                    { 6, "Analyzed data sets and created data reports", 4, 3, "Log 2" },
                    { 7, "Learned SQL database optimization techniques", 2, 4, "Log 1" },
                    { 8, "Focused on advanced SQL queries for interviews", 2, 4, "Log 2" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "Status", "Title", "UserInterviewId" },
                values: new object[,]
                {
                    { 1, "Need to smile more on interviews", "Reviewed", "Quick note from first interview", 1 },
                    { 2, "Reviewed algorithms and problem-solving questions", "Reviewed", "Technical question review", 2 },
                    { 3, "Need to work on STAR method for behavioral questions", "NotReviewed", "Behavioral question notes", 3 },
                    { 4, "Worked on cleaning data sets for the upcoming interview", "NotReviewed", "Data analysis feedback", 4 },
                    { 5, "Reviewed optimization techniques and SQL queries", "Reviewed", "SQL skills review", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_UserId",
                table: "Logbooks",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_InterviewId",
                table: "Logs",
                column: "InterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LogbookId",
                table: "Logs",
                column: "LogbookId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserInterviewId",
                table: "Notes",
                column: "UserInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterviews_InterviewId",
                table: "UserInterviews",
                column: "InterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterviews_UserId",
                table: "UserInterviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Logbooks");

            migrationBuilder.DropTable(
                name: "UserInterviews");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
