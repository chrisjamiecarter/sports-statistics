using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStatistics.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddFixtureTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_SquadNumber",
                schema: "sports",
                table: "Players");

            migrationBuilder.CreateTable(
                name: "Fixtures",
                schema: "sports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KickoffTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score_HomeGoals = table.Column<int>(type: "int", nullable: false),
                    Score_AwayGoals = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fixtures",
                schema: "sports");

            migrationBuilder.CreateIndex(
                name: "IX_Players_SquadNumber",
                schema: "sports",
                table: "Players",
                column: "SquadNumber",
                unique: true);
        }
    }
}
