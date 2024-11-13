using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Interview_Server.Migrations
{
    /// <inheritdoc />
    public partial class addedDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "UserInterviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "UserInterviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "DurationInMinutes",
                value: 120);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "UserInterviews");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPSc4wNR4girynWl18H6d77IS8t+ATyGgAJlVyKSIlObLMCzWSx+n5AZKgzRrZpzdA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPp9YbwMQYFPyzcKkvrw/CRFNw/fDCod5RdkWwCW12jdoB8qpxN7nmI4azGdqZuQqg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAECzQ/woUf17bR78lZo0HF7IGuQEyxzU3qz6btPG9GIwHQ1MCNQg7uRWMQaH8VJt6bw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAkbEc79745+Jx3blcDpzUNHV/CjSNVc41eKazaW1uBRDK7B1G1x6brxJuWT37G6vw==");
        }
    }
}
