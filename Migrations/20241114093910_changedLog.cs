using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class changedLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_UserInterviews_UserInterviewId",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "UserInterviewId",
                table: "Logs",
                newName: "InterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_UserInterviewId",
                table: "Logs",
                newName: "IX_Logs_InterviewId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFo1KCnr/pQWqBIf2f5GblwtDjQp45J89Yqy7MNswcX8ajD2/V8nRBRAYIkD9OM+hQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFmnPKujoYk6f0oRhk7mlcXCWQ+UDZk2yf+SJPkFnigE6KgP1PrXwtcIL3Ct/zaIMg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAED/Rl6ypOMTEqgi+67Kn+WP4Qb/P5M10S0rKZ3PkCMZHxvaTSEctqvIkiVhWBOjPng==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEI/mWSNIcmjz05Gv1yDpWhqBMglDPafBNLnGWZp7nxapAiTHz36/3cN16OR5ANplGA==");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Interviews_InterviewId",
                table: "Logs",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Interviews_InterviewId",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "InterviewId",
                table: "Logs",
                newName: "UserInterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_InterviewId",
                table: "Logs",
                newName: "IX_Logs_UserInterviewId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_UserInterviews_UserInterviewId",
                table: "Logs",
                column: "UserInterviewId",
                principalTable: "UserInterviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
