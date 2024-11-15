using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class addedLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logbooks_UserInterviews_UserInterviewId",
                table: "Logbooks");

            migrationBuilder.DropIndex(
                name: "IX_Logbooks_UserInterviewId",
                table: "Logbooks");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Logbooks");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Logbooks");

            migrationBuilder.DropColumn(
                name: "UserInterviewId",
                table: "Logbooks");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogbookId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    UserInterviewId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Logbooks_LogbookId",
                        column: x => x.LogbookId,
                        principalTable: "Logbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logs_UserInterviews_UserInterviewId",
                        column: x => x.UserInterviewId,
                        principalTable: "UserInterviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Logbooks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Logbook1");

            migrationBuilder.InsertData(
                table: "Logs",
                columns: new[] { "Id", "Content", "LogbookId", "Title", "UserInterviewId" },
                values: new object[,]
                {
                    { 1, "Learned x and y", 1, "Log 1", 1 },
                    { 2, "Learned z and i", 1, "Log 2", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEG8Ss4fmhjAFeuvGr4Pt6TuvUKoKNvMdDKfUSrWYrnLjueGmy3sNu6VZrxgGBdcyNw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPDGSQubrc9oyMUV+LK2aaJUp45fUWERKpIp984UIYbHOnxdw+zvTbZ81oipPC8SKA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEP+JRKat7J5X4Dq+2qWuorSqf+ZQ3x72LmL2vewzDXTY7ptfZi3RMw2HaBvqHNFBQQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELn2DRhfswNDoPxW98cl8J2Pv40g/9LZSeW8I62clUYHJwsaz6GeV3p+L6/8tZbOgw==");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LogbookId",
                table: "Logs",
                column: "LogbookId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserInterviewId",
                table: "Logs",
                column: "UserInterviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Logbooks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "Logbooks",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "UserInterviewId",
                table: "Logbooks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Logbooks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Time", "Title", "UserInterviewId" },
                values: new object[] { "Overall Good Interview. Need to improve something.", new TimeOnly(14, 30, 0), "Logbook from first interview", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMUxNL4P/WnD0wWJMUPFjf6zlUmhQ0Ay9D5XIPlxDp1zSu3Pd8IG8xJyIC8law3Piw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOJUesdY6WENCJ1kByVPhHUZbu7Y+4Ez1urqTY7yRD4kKUXq0eD88GWSGq5HgnFyvg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAO255Ct7sJVKG/wke49idyOVhnbtOn36ZiyAz5QUrg9sAtR7RVD1nxkA/F6z3q7Cg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEK3wCpvYlrr6u1GOJC7jTexsvUi3CHi1pAXBfGhnzRrEntyJ0jPB83xtt54wWoHy6w==");

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_UserInterviewId",
                table: "Logbooks",
                column: "UserInterviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logbooks_UserInterviews_UserInterviewId",
                table: "Logbooks",
                column: "UserInterviewId",
                principalTable: "UserInterviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
