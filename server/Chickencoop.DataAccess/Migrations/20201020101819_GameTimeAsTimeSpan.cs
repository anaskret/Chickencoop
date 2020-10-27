using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chickencoop.DataAccess.Migrations
{
    public partial class GameTimeAsTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "GameTime",
                table: "PersonalLeaderboards",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "GameTime",
                table: "PersonalLeaderboards",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
