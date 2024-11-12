using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    InterviewId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.InterviewId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Mobile = table.Column<string>(type: "text", nullable: false),
                    LogbookId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserInterviews",
                columns: table => new
                {
                    UserInterviewId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    InterviewId = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    InterviewTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterviews", x => x.UserInterviewId);
                    table.ForeignKey(
                        name: "FK_UserInterviews_Interviews_InterviewId",
                        column: x => x.InterviewId,
                        principalTable: "Interviews",
                        principalColumn: "InterviewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInterviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logbooks",
                columns: table => new
                {
                    LogbookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserInterviewId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logbooks", x => x.LogbookId);
                    table.ForeignKey(
                        name: "FK_Logbooks_UserInterviews_UserInterviewId",
                        column: x => x.UserInterviewId,
                        principalTable: "UserInterviews",
                        principalColumn: "UserInterviewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logbooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    UserInterviewId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_UserInterviews_UserInterviewId",
                        column: x => x.UserInterviewId,
                        principalTable: "UserInterviews",
                        principalColumn: "UserInterviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interviews",
                columns: new[] { "InterviewId", "Address", "CompanyName", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Kongens gate 6", "PayEx", "Technical interview after a short speedinterview", "Technical Interview" },
                    { 2, "Idrettsveien 8", "Nordre Follo Kommune", "Bli kjent intervju", "Førstegangsintervju" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "LogbookId", "Mobile", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "ali@example.com", 1, "1234", "AQAAAAIAAYagAAAAEOZszi4gKqJzlQvjcY5Og92KnldgiyBt0sHqjI//BfuEkqEiZZ2gB+BQXXBrHQ6Naw==", "Ali Khan" },
                    { 2, "muaath@example.com", 2, "1881", "AQAAAAIAAYagAAAAEMrhSbS+JtweMenpeWDfbFHirWOrLP45TJJY4Q2DLmpi4utqbTCbaXVSoouCg43YIA==", "Muaath Zerouga" },
                    { 3, "john@example.com", 3, "123", "AQAAAAIAAYagAAAAEKiCSfTaIOEvyuMxCPireJDR7SlYJLQJCJV5/QMkrVO7Gg0H8aOUlgoES/nQi2M0JQ==", "John Ferdie" },
                    { 4, "magnus@example.com", 4, "786", "AQAAAAIAAYagAAAAEJiPAImFwTzy5jndnssJY6WLeGi9iPFd/334APSI606kGtD+R9h5dh+QkMt7FtcrLg==", "Magnus Brandsegg" }
                });

            migrationBuilder.InsertData(
                table: "UserInterviews",
                columns: new[] { "UserInterviewId", "InterviewId", "InterviewTime", "Role", "Status", "UserId" },
                values: new object[] { 1, 1, new DateTime(2024, 11, 11, 14, 30, 0, 0, DateTimeKind.Utc), "Interviewee", "Scheduled", 1 });

            migrationBuilder.InsertData(
                table: "Logbooks",
                columns: new[] { "LogbookId", "Content", "Time", "Title", "UserId", "UserInterviewId" },
                values: new object[] { 1, "Overall Good Interview. Need to improve something.", new TimeOnly(14, 30, 0), "Logbook from first interview", 1, 1 });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "NoteId", "Content", "Status", "Title", "UserInterviewId" },
                values: new object[] { 1, "Need to smile more on interviews", "Reviewed", "Quick note from first interview", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_UserId",
                table: "Logbooks",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_UserInterviewId",
                table: "Logbooks",
                column: "UserInterviewId");

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
                name: "Logbooks");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "UserInterviews");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
