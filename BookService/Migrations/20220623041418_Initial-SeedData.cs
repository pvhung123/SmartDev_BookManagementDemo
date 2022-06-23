using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookService.Migrations
{
    public partial class InitialSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "CreatedDate", "Description", "ModifiedDate", "Title" },
                values: new object[,]
                {
                    { 1, "Author 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# book description", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Book" },
                    { 2, "Author 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java book description", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java" },
                    { 3, "Author 3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PHP book description", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PHP" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "CreatedDate", "Fullname", "ModifiedDate", "Username" },
                values: new object[,]
                {
                    { 1, "HCM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { 2, "DN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pham Van Hung", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hungpv" }
                });

            migrationBuilder.InsertData(
                table: "UserBook",
                columns: new[] { "Id", "BookId", "CreatedDate", "ModifiedDate", "ReadingStatus", "UserId" },
                values: new object[] { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 });

            migrationBuilder.InsertData(
                table: "UserBook",
                columns: new[] { "Id", "BookId", "CreatedDate", "ModifiedDate", "ReadingStatus", "UserId" },
                values: new object[] { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 });

            migrationBuilder.InsertData(
                table: "UserBook",
                columns: new[] { "Id", "BookId", "CreatedDate", "ModifiedDate", "ReadingStatus", "UserId" },
                values: new object[] { 3, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
