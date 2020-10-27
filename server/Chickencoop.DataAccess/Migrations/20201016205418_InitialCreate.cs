using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chickencoop.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nickname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalLeaderboards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GameTime = table.Column<DateTime>(nullable: false),
                    GameDate = table.Column<DateTime>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    OpponentId = table.Column<Guid>(nullable: false),
                    ResultTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalLeaderboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalLeaderboards_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalLeaderboards_ResultTypes_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalLeaderboards_PlayerId",
                table: "PersonalLeaderboards",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalLeaderboards_ResultTypeId",
                table: "PersonalLeaderboards",
                column: "ResultTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalLeaderboards");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "ResultTypes");
        }
    }
}
