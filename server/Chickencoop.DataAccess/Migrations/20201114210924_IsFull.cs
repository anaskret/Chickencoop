using Microsoft.EntityFrameworkCore.Migrations;

namespace Chickencoop.DataAccess.Migrations
{
    public partial class IsFull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isFull",
                table: "Lobbies",
                newName: "IsFull");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFull",
                table: "Lobbies",
                newName: "isFull");
        }
    }
}
