using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chickencoop.DataAccess.Migrations
{
    public partial class ResultTypesAsEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalLeaderboards_ResultTypes_ResultTypeId",
                table: "PersonalLeaderboards");

            migrationBuilder.DropTable(
                name: "ResultTypes");

            migrationBuilder.DropIndex(
                name: "IX_PersonalLeaderboards_ResultTypeId",
                table: "PersonalLeaderboards");

            migrationBuilder.DropColumn(
                name: "ResultTypeId",
                table: "PersonalLeaderboards");

            migrationBuilder.AddColumn<int>(
                name: "Result",
                table: "PersonalLeaderboards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "PersonalLeaderboards");

            migrationBuilder.AddColumn<Guid>(
                name: "ResultTypeId",
                table: "PersonalLeaderboards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ResultTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalLeaderboards_ResultTypeId",
                table: "PersonalLeaderboards",
                column: "ResultTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalLeaderboards_ResultTypes_ResultTypeId",
                table: "PersonalLeaderboards",
                column: "ResultTypeId",
                principalTable: "ResultTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
