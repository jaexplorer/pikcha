using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PikchaWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "ImageTags",
                columns: table => new
                {
                    ImageTagId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTags", x => x.ImageTagId);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PikchaRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PikchaUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Fname = table.Column<string>(nullable: true),
                    Lname = table.Column<string>(nullable: true),
                    AvFile = table.Column<string>(nullable: true),
                    SigFile = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    PerAddr1 = table.Column<string>(nullable: true),
                    PerAddr2 = table.Column<string>(nullable: true),
                    PerCity = table.Column<string>(nullable: true),
                    PerPostal = table.Column<string>(nullable: true),
                    PerCountry = table.Column<string>(nullable: true),
                    ShipAddr1 = table.Column<string>(nullable: true),
                    ShipAddr2 = table.Column<string>(nullable: true),
                    ShipCity = table.Column<string>(nullable: true),
                    ShipPostal = table.Column<string>(nullable: true),
                    ShipCountry = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Insta = table.Column<string>(nullable: true),
                    LinkdIn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PikchaRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PikchaRoleClaims_PikchaRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "PikchaRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PikchaImages",
                columns: table => new
                {
                    PikchaImageId = table.Column<string>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    NoOfPrint = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    ThumbFile = table.Column<string>(nullable: true),
                    WaterFile = table.Column<string>(nullable: true),
                    UploadedAt = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "getdate()"),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "getdate()"),
                    ArtistId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaImages", x => x.PikchaImageId);
                    table.ForeignKey(
                        name: "FK_PikchaImages_PikchaUsers_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "PikchaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PikchaUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PikchaUserClaims_PikchaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PikchaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PikchaUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_PikchaUserLogins_PikchaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PikchaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PikchaUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PikchaUserRoles_PikchaRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "PikchaRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PikchaUserRoles_PikchaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PikchaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PikchaUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_PikchaUserTokens_PikchaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PikchaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PikchaImageTags",
                columns: table => new
                {
                    PikchaImageTagId = table.Column<long>(nullable: false),
                    ImageTagId = table.Column<long>(nullable: true),
                    PikchaImageId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaImageTags", x => x.PikchaImageTagId);
                    table.ForeignKey(
                        name: "FK_PikchaImageTags_ImageTags_ImageTagId",
                        column: x => x.ImageTagId,
                        principalTable: "ImageTags",
                        principalColumn: "ImageTagId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PikchaImageTags_PikchaImages_PikchaImageId",
                        column: x => x.PikchaImageId,
                        principalTable: "PikchaImages",
                        principalColumn: "PikchaImageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PikchaImageViews",
                columns: table => new
                {
                    PikchaImageViewId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Count = table.Column<long>(nullable: false),
                    PikchaImageId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaImageViews", x => x.PikchaImageViewId);
                    table.ForeignKey(
                        name: "FK_PikchaImageViews_PikchaImages_PikchaImageId",
                        column: x => x.PikchaImageId,
                        principalTable: "PikchaImages",
                        principalColumn: "PikchaImageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PikchaImages_ArtistId",
                table: "PikchaImages",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaImageTags_ImageTagId",
                table: "PikchaImageTags",
                column: "ImageTagId");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaImageTags_PikchaImageId",
                table: "PikchaImageTags",
                column: "PikchaImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaImageViews_PikchaImageId",
                table: "PikchaImageViews",
                column: "PikchaImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaRoleClaims_RoleId",
                table: "PikchaRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "PikchaRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaUserClaims_UserId",
                table: "PikchaUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaUserLogins_UserId",
                table: "PikchaUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaUserRoles_RoleId",
                table: "PikchaUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "PikchaUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "PikchaUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "PikchaImageTags");

            migrationBuilder.DropTable(
                name: "PikchaImageViews");

            migrationBuilder.DropTable(
                name: "PikchaRoleClaims");

            migrationBuilder.DropTable(
                name: "PikchaUserClaims");

            migrationBuilder.DropTable(
                name: "PikchaUserLogins");

            migrationBuilder.DropTable(
                name: "PikchaUserRoles");

            migrationBuilder.DropTable(
                name: "PikchaUserTokens");

            migrationBuilder.DropTable(
                name: "ImageTags");

            migrationBuilder.DropTable(
                name: "PikchaImages");

            migrationBuilder.DropTable(
                name: "PikchaRoles");

            migrationBuilder.DropTable(
                name: "PikchaUsers");
        }
    }
}
