using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedmigration : Migration
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
                    { 1, "Kongens gate 6", "PayEx", "Technical interview after a short speedinterview", "Technical Interview" },
                    { 2, "Idrettsveien 8", "Nordre Follo Kommune", "Bli kjent intervju", "Førstegangsintervju" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LogbookId", "Mobile", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "ali@example.com", 1, "1234", "AQAAAAIAAYagAAAAEL2sVocRz0ca2jN7Eoboh0zhOwDQ84jtb6YvpBp+lEIKIEFWgttoiTrB/W3iYpqYLg==", "Ali Khan" },
                    { 2, "muaath@example.com", 2, "1881", "AQAAAAIAAYagAAAAEDa/g3TbbLRRoXC4YwhKi0vosgzhuOF7Hph5n9LMg36KEgx6F8xFGYjsh4PXiUfKlQ==", "Muaath Zerouga" },
                    { 3, "john@example.com", 3, "123", "AQAAAAIAAYagAAAAEOoJFr7gG+/doWT0DsoDY1b0dErZgB7KJwFG8Uv4lVfWza7x6sye3SR5a/9e3qtiaQ==", "John Ferdie" },
                    { 4, "magnus@example.com", 4, "786", "AQAAAAIAAYagAAAAEJyUcjAxI5gT9/kqHfJCBZQXs1plEHYWbwV+JQxTL8alMn3zcJgZVxEYrzMcD9RuQA==", "Magnus Brandsegg" }
                });

            migrationBuilder.InsertData(
                table: "Logbooks",
                columns: new[] { "Id", "Title", "UserId" },
                values: new object[] { 1, "Logbook1", 1 });

            migrationBuilder.InsertData(
                table: "UserInterviews",
                columns: new[] { "Id", "DurationInMinutes", "InterviewId", "InterviewTime", "Role", "Status", "UserId" },
                values: new object[] { 1, 120, 1, new DateTime(2024, 11, 11, 14, 30, 0, 0, DateTimeKind.Utc), "Interviewee", "Scheduled", 1 });

            migrationBuilder.InsertData(
                table: "Logs",
                columns: new[] { "Id", "Content", "InterviewId", "LogbookId", "Title" },
                values: new object[,]
                {
                    { 1, "Learned x and y", 1, 1, "Log 1" },
                    { 2, "Learned z and i", 1, 1, "Log 2" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "Content", "Status", "Title", "UserInterviewId" },
                values: new object[] { 1, "Need to smile more on interviews", "Reviewed", "Quick note from first interview", 1 });

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
