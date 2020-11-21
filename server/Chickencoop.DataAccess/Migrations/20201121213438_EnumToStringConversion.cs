using Microsoft.EntityFrameworkCore.Migrations;

namespace Chickencoop.DataAccess.Migrations
{
    public partial class EnumToStringConversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "PersonalLeaderboards",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GameName",
                table: "PersonalLeaderboards",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GameName",
                table: "Lobbies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Result",
                table: "PersonalLeaderboards",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "GameName",
                table: "PersonalLeaderboards",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "GameName",
                table: "Lobbies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
