using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CA1861, IDE0300

namespace SportsStatistics.Infrastructure.Migrations;

/// <inheritdoc />
public partial class PlayersEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        ArgumentNullException.ThrowIfNull(migrationBuilder, nameof(migrationBuilder));

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            table: "AspNetRoleClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            table: "AspNetUserClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            table: "AspNetUserLogins");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            table: "AspNetUserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            table: "AspNetUserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            table: "AspNetUserTokens");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUserTokens",
            table: "AspNetUserTokens");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUsers",
            table: "AspNetUsers");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUserRoles",
            table: "AspNetUserRoles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUserLogins",
            table: "AspNetUserLogins");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUserClaims",
            table: "AspNetUserClaims");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetRoles",
            table: "AspNetRoles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetRoleClaims",
            table: "AspNetRoleClaims");

        migrationBuilder.EnsureSchema(
            name: "sports");

        migrationBuilder.EnsureSchema(
            name: "identity");

        migrationBuilder.RenameTable(
            name: "AspNetUserTokens",
            newName: "UserTokens",
            newSchema: "identity");

        migrationBuilder.RenameTable(
            name: "AspNetUsers",
            newName: "Users",
            newSchema: "identity");

        migrationBuilder.RenameTable(
            name: "AspNetUserRoles",
            newName: "UserRoles",
            newSchema: "identity");

        migrationBuilder.RenameTable(
            name: "AspNetUserLogins",
            newName: "UserLogin",
            newSchema: "identity");

        migrationBuilder.RenameTable(
            name: "AspNetUserClaims",
            newName: "UserClaims",
            newSchema: "identity");

        migrationBuilder.RenameTable(
            name: "AspNetRoles",
            newName: "Roles",
            newSchema: "identity");

        migrationBuilder.RenameTable(
            name: "AspNetRoleClaims",
            newName: "RoleClaims",
            newSchema: "identity");

        migrationBuilder.RenameIndex(
            name: "IX_AspNetUserRoles_RoleId",
            schema: "identity",
            table: "UserRoles",
            newName: "IX_UserRoles_RoleId");

        migrationBuilder.RenameIndex(
            name: "IX_AspNetUserLogins_UserId",
            schema: "identity",
            table: "UserLogin",
            newName: "IX_UserLogin_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_AspNetUserClaims_UserId",
            schema: "identity",
            table: "UserClaims",
            newName: "IX_UserClaims_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            schema: "identity",
            table: "RoleClaims",
            newName: "IX_RoleClaims_RoleId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_UserTokens",
            schema: "identity",
            table: "UserTokens",
            columns: new[] { "UserId", "LoginProvider", "Name" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_Users",
            schema: "identity",
            table: "Users",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_UserRoles",
            schema: "identity",
            table: "UserRoles",
            columns: new[] { "UserId", "RoleId" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_UserLogin",
            schema: "identity",
            table: "UserLogin",
            columns: new[] { "LoginProvider", "ProviderKey" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_UserClaims",
            schema: "identity",
            table: "UserClaims",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Roles",
            schema: "identity",
            table: "Roles",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_RoleClaims",
            schema: "identity",
            table: "RoleClaims",
            column: "Id");

        migrationBuilder.CreateTable(
            name: "Players",
            schema: "sports",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                SquadNumber = table.Column<int>(type: "int", nullable: false),
                Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Players", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Players_SquadNumber",
            schema: "sports",
            table: "Players",
            column: "SquadNumber",
            unique: true);

        migrationBuilder.AddForeignKey(
            name: "FK_RoleClaims_Roles_RoleId",
            schema: "identity",
            table: "RoleClaims",
            column: "RoleId",
            principalSchema: "identity",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_UserClaims_Users_UserId",
            schema: "identity",
            table: "UserClaims",
            column: "UserId",
            principalSchema: "identity",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_UserLogin_Users_UserId",
            schema: "identity",
            table: "UserLogin",
            column: "UserId",
            principalSchema: "identity",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_UserRoles_Roles_RoleId",
            schema: "identity",
            table: "UserRoles",
            column: "RoleId",
            principalSchema: "identity",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_UserRoles_Users_UserId",
            schema: "identity",
            table: "UserRoles",
            column: "UserId",
            principalSchema: "identity",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_UserTokens_Users_UserId",
            schema: "identity",
            table: "UserTokens",
            column: "UserId",
            principalSchema: "identity",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        ArgumentNullException.ThrowIfNull(migrationBuilder, nameof(migrationBuilder));

        migrationBuilder.DropForeignKey(
            name: "FK_RoleClaims_Roles_RoleId",
            schema: "identity",
            table: "RoleClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_UserClaims_Users_UserId",
            schema: "identity",
            table: "UserClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_UserLogin_Users_UserId",
            schema: "identity",
            table: "UserLogin");

        migrationBuilder.DropForeignKey(
            name: "FK_UserRoles_Roles_RoleId",
            schema: "identity",
            table: "UserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_UserRoles_Users_UserId",
            schema: "identity",
            table: "UserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_UserTokens_Users_UserId",
            schema: "identity",
            table: "UserTokens");

        migrationBuilder.DropTable(
            name: "Players",
            schema: "sports");

        migrationBuilder.DropPrimaryKey(
            name: "PK_UserTokens",
            schema: "identity",
            table: "UserTokens");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Users",
            schema: "identity",
            table: "Users");

        migrationBuilder.DropPrimaryKey(
            name: "PK_UserRoles",
            schema: "identity",
            table: "UserRoles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_UserLogin",
            schema: "identity",
            table: "UserLogin");

        migrationBuilder.DropPrimaryKey(
            name: "PK_UserClaims",
            schema: "identity",
            table: "UserClaims");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Roles",
            schema: "identity",
            table: "Roles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_RoleClaims",
            schema: "identity",
            table: "RoleClaims");

        migrationBuilder.RenameTable(
            name: "UserTokens",
            schema: "identity",
            newName: "AspNetUserTokens");

        migrationBuilder.RenameTable(
            name: "Users",
            schema: "identity",
            newName: "AspNetUsers");

        migrationBuilder.RenameTable(
            name: "UserRoles",
            schema: "identity",
            newName: "AspNetUserRoles");

        migrationBuilder.RenameTable(
            name: "UserLogin",
            schema: "identity",
            newName: "AspNetUserLogins");

        migrationBuilder.RenameTable(
            name: "UserClaims",
            schema: "identity",
            newName: "AspNetUserClaims");

        migrationBuilder.RenameTable(
            name: "Roles",
            schema: "identity",
            newName: "AspNetRoles");

        migrationBuilder.RenameTable(
            name: "RoleClaims",
            schema: "identity",
            newName: "AspNetRoleClaims");

        migrationBuilder.RenameIndex(
            name: "IX_UserRoles_RoleId",
            table: "AspNetUserRoles",
            newName: "IX_AspNetUserRoles_RoleId");

        migrationBuilder.RenameIndex(
            name: "IX_UserLogin_UserId",
            table: "AspNetUserLogins",
            newName: "IX_AspNetUserLogins_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_UserClaims_UserId",
            table: "AspNetUserClaims",
            newName: "IX_AspNetUserClaims_UserId");

        migrationBuilder.RenameIndex(
            name: "IX_RoleClaims_RoleId",
            table: "AspNetRoleClaims",
            newName: "IX_AspNetRoleClaims_RoleId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUserTokens",
            table: "AspNetUserTokens",
            columns: new[] { "UserId", "LoginProvider", "Name" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUsers",
            table: "AspNetUsers",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUserRoles",
            table: "AspNetUserRoles",
            columns: new[] { "UserId", "RoleId" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUserLogins",
            table: "AspNetUserLogins",
            columns: new[] { "LoginProvider", "ProviderKey" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUserClaims",
            table: "AspNetUserClaims",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetRoles",
            table: "AspNetRoles",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetRoleClaims",
            table: "AspNetRoleClaims",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId",
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            table: "AspNetUserClaims",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            table: "AspNetUserLogins",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId",
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            table: "AspNetUserRoles",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            table: "AspNetUserTokens",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
