using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class updateddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash", "Username" },
                values: new object[] { "ali@example.com", "AQAAAAIAAYagAAAAEJ91hjAbshFeSGUvyPTqYJx+N1IR5YHx7oesLxIOSyk9xYW4rGu28QvudrXwY5zqaQ==", "Ali Haider Khan" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Email", "PasswordHash", "Username" },
                values: new object[] { "muaath@example.com", "AQAAAAIAAYagAAAAEHCdsx0Cx0l2+jbt4IZY31N7oUhb65XxA3+FsL8FN/5bBE/bz0C7z7JXRRStF9JbbA==", "Muaath Zerouga" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash", "Username" },
                values: new object[] { "john@example.com", "AQAAAAIAAYagAAAAEPf0BPaeT0b+qs7RuyHK1Z/4Xjhtn0f9/oN8UAvZ0/pM9OnXznGa0KXir922sl3Gbg==", "John Doe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Email", "PasswordHash", "Username" },
                values: new object[] { "jane@example.com", "AQAAAAIAAYagAAAAEP8WVY7easC27anrD3gX9WnAiDgY3I01GZy4R7GWjBbhya9wDyjJZlFk8AItFb9pBA==", "Jane Smith" });
        }
    }
}
