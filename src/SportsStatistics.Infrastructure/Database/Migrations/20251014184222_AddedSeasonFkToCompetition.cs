using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStatistics.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeasonFkToCompetition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "sports",
                table: "Fixtures",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "sports",
                table: "Competitions",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "SeasonId",
                schema: "sports",
                table: "Competitions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_CompetitionId",
                schema: "sports",
                table: "Fixtures",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_SeasonId",
                schema: "sports",
                table: "Competitions",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Seasons_SeasonId",
                schema: "sports",
                table: "Competitions",
                column: "SeasonId",
                principalSchema: "sports",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Competitions_CompetitionId",
                schema: "sports",
                table: "Fixtures",
                column: "CompetitionId",
                principalSchema: "sports",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Seasons_SeasonId",
                schema: "sports",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Competitions_CompetitionId",
                schema: "sports",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_CompetitionId",
                schema: "sports",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_SeasonId",
                schema: "sports",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                schema: "sports",
                table: "Competitions");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "sports",
                table: "Fixtures",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "sports",
                table: "Competitions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);
        }
    }
}
