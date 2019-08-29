using Microsoft.EntityFrameworkCore.Migrations;

namespace PikchaWebApp.Data.Migrations
{
    public partial class addinguserprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PikchaRoleClaims_PikchaRoles_RoleId",
                table: "PikchaRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_PikchaUserClaims_PikchaUsers_UserId",
                table: "PikchaUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_PikchaUserLogins_PikchaUsers_UserId",
                table: "PikchaUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_PikchaUserRoles_PikchaRoles_RoleId",
                table: "PikchaUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PikchaUserRoles_PikchaUsers_UserId",
                table: "PikchaUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PikchaUserTokens_PikchaUsers_UserId",
                table: "PikchaUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikchaUserTokens",
                table: "PikchaUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikchaUsers",
                table: "PikchaUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikchaUserRoles",
                table: "PikchaUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikchaUserLogins",
                table: "PikchaUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikchaUserClaims",
                table: "PikchaUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikchaRoles",
                table: "PikchaRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PikchaRoleClaims",
                table: "PikchaRoleClaims");

            migrationBuilder.RenameTable(
                name: "PikchaUserTokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "PikchaUsers",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "PikchaUserRoles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "PikchaUserLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "PikchaUserClaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "PikchaRoles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "PikchaRoleClaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_PikchaUserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_PikchaUserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PikchaUserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PikchaRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddColumn<string>(
                name: "Address_1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_2",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarFileName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BioInfo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookLink",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramLink",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInLink",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatureFileName",
                table: "AspNetUsers",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Address_1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Address_2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarFileName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BioInfo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FacebookLink",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstagramLink",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedInLink",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SignatureFileName",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "PikchaUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "PikchaUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "PikchaUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "PikchaUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "PikchaUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "PikchaRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "PikchaRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "PikchaUserRoles",
                newName: "IX_PikchaUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "PikchaUserLogins",
                newName: "IX_PikchaUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "PikchaUserClaims",
                newName: "IX_PikchaUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "PikchaRoleClaims",
                newName: "IX_PikchaRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikchaUserTokens",
                table: "PikchaUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikchaUsers",
                table: "PikchaUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikchaUserRoles",
                table: "PikchaUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikchaUserLogins",
                table: "PikchaUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikchaUserClaims",
                table: "PikchaUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikchaRoles",
                table: "PikchaRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PikchaRoleClaims",
                table: "PikchaRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PikchaRoleClaims_PikchaRoles_RoleId",
                table: "PikchaRoleClaims",
                column: "RoleId",
                principalTable: "PikchaRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PikchaUserClaims_PikchaUsers_UserId",
                table: "PikchaUserClaims",
                column: "UserId",
                principalTable: "PikchaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PikchaUserLogins_PikchaUsers_UserId",
                table: "PikchaUserLogins",
                column: "UserId",
                principalTable: "PikchaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PikchaUserRoles_PikchaRoles_RoleId",
                table: "PikchaUserRoles",
                column: "RoleId",
                principalTable: "PikchaRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PikchaUserRoles_PikchaUsers_UserId",
                table: "PikchaUserRoles",
                column: "UserId",
                principalTable: "PikchaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PikchaUserTokens_PikchaUsers_UserId",
                table: "PikchaUserTokens",
                column: "UserId",
                principalTable: "PikchaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
