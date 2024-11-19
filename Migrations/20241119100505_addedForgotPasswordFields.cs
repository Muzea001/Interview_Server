using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class addedForgotPasswordFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiry",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "ResetToken", "ResetTokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAEJa0KSGVXLXNhoY70CSHNno+q+QAQ/DTFnu2vdsSWzTvZD16plNmzuHyr0+YVGzEAw==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "ResetToken", "ResetTokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAEJxX5gnripEu+GW+GFps2LlieMMlM3xIUn3o98mW+V239mHxz4l0ndNaPqkMXtohXw==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "ResetToken", "ResetTokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAENOSqY/gVIIOEbPd0Qnujc1/HulSwM4Z/90fGNxmwkASsUTMtoihj5mzYc/lt5AIxw==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "ResetToken", "ResetTokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAELOPLzA42cFXQ9rCx36FLRNjOGRV1G0k7KP2GxA+LAhtgCuT5UE3VXhTRiKE5A6A3A==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "ResetToken", "ResetTokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAEE38v+QZtKTumUvtTu2cj+KVQLj9eGGBjlz3w2Wicy0ShiRrb3OpZdShqk8VvmCo9w==", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PasswordHash", "ResetToken", "ResetTokenExpiry" },
                values: new object[] { "AQAAAAIAAYagAAAAEF3Z9OdMt2/4USmvD/TBmfaFEz3bWRCnTBtnjYtp5lyIqaQ6ez9GV0YHpR3P41qnVQ==", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpiry",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMsgNFWhy/ccML643/4u1sPAX037huLK890/V3h1iQvllwZtH1DR4AHLxwMfCO4kCg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEA5qnmUrjkoz0hOtBJpmyIQHqGak1SxGKdX7qyEUnyiI+j8iu+f8mv1asitS/xAAcQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFvEbhgmmRX3zSiJ7ozixORKf4DAbB+jcLGZHMhXD7D7bO0esOxnAXdB/1N7jlt5lQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEH4nyhKF+X+N938Hcw5qxFTb4PjtP0YjlI/eT/wgepwHAN7RpiNVExadgHX3anPZ+g==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHS3E8x0pU/L1wkZDoR3uPDVbsHIRhtGgoPyQbPRmmPwkZ0Jw3jAIRrgRmc7tUmazA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPBqRxOS+Yy49mLSkVZjkypconXTA3un2n2OfZgznE/cF4z6b4e9TIi0q/ZfPCHY2Q==");
        }
    }
}
