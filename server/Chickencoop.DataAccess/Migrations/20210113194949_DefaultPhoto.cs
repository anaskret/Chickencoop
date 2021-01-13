using Microsoft.EntityFrameworkCore.Migrations;

namespace Chickencoop.DataAccess.Migrations
{
    public partial class DefaultPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BackgroundUrl",
                table: "Players",
                nullable: true,
                defaultValue: "https://cdn.vuetifyjs.com/images/cards/server-room.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Players",
                nullable: true,
                defaultValue: "https://cdn.vuetifyjs.com/images/profiles/marcus.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BackgroundUrl",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "https://cdn.vuetifyjs.com/images/cards/server-room.jpg");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "https://cdn.vuetifyjs.com/images/profiles/marcus.jpg");
        }
    }
}
