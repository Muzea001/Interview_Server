using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "AQAAAAIAAYagAAAAECdWjUPosBQOXnfTia45iq8ALvYL9lGXpUEFZ9zNEluSsEdrCeeROV+GmbPEbDvROw==", "Ali Khan" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEATRDu7yVbHy1Sl0qYl34KqD4xUvgItRTS0dwtPj6/KHDvdxqD89zHLSLNUNBclaMg==");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "LogbookId", "Mobile", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 3, "john@example.com", 3, "123", "AQAAAAIAAYagAAAAECs4CZIyZEsA8/YgGpeaUYlWdEzJ3/qVyk7+NSam9Zp2bObY6BP0cX1EXaIKiF0YCg==", "John Ferdie" },
                    { 4, "magnus@example.com", 4, "786", "AQAAAAIAAYagAAAAELZXaa16Q5y94U7PLsL6V2GtDndK+BnGLoHH2OJQHRZG9yNNe7ODm1NCllcpYJTcsA==", "Magnus Brandsegg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "AQAAAAIAAYagAAAAEJ91hjAbshFeSGUvyPTqYJx+N1IR5YHx7oesLxIOSyk9xYW4rGu28QvudrXwY5zqaQ==", "Ali Haider Khan" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHCdsx0Cx0l2+jbt4IZY31N7oUhb65XxA3+FsL8FN/5bBE/bz0C7z7JXRRStF9JbbA==");
        }
    }
}
