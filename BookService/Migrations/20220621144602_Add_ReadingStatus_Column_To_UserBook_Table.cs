using Microsoft.EntityFrameworkCore.Migrations;

namespace BookService.Migrations
{
    public partial class Add_ReadingStatus_Column_To_UserBook_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadingStatus",
                table: "UserBook",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingStatus",
                table: "UserBook");
        }
    }
}
