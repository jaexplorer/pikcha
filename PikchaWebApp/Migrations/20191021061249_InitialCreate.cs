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
                    Avatar = table.Column<string>(nullable: true),
                    Sign = table.Column<string>(nullable: true),
                    InvSign = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    Addr1 = table.Column<string>(nullable: true),
                    Addr2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Postal = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Links = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
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
                name: "ArtistFollowers",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ArtistsId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistFollowers", x => new { x.ArtistsId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ArtistFollowers_PikchaUsers_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "PikchaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtistFollowers_PikchaUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "PikchaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Thumbnail = table.Column<string>(nullable: true),
                    Watermark = table.Column<string>(nullable: true),
                    UploadedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: false),
                    ArtistId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_PikchaUsers_ArtistId",
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
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
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
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
                name: "ImageProducts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsSale = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true),
                    SellerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageProducts_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageProducts_PikchaUsers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "PikchaUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageTags",
                columns: table => new
                {
                    ImageTagId = table.Column<string>(nullable: false),
                    PikchaImageId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTags", x => new { x.ImageTagId, x.PikchaImageId });
                    table.ForeignKey(
                        name: "FK_ImageTags_Tags_ImageTagId",
                        column: x => x.ImageTagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageTags_Images_PikchaImageId",
                        column: x => x.PikchaImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageViews",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    PikchaImageId = table.Column<string>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageViews", x => new { x.Date, x.PikchaImageId });
                    table.ForeignKey(
                        name: "FK_ImageViews_Images_PikchaImageId",
                        column: x => x.PikchaImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistFollowers_UserId",
                table: "ArtistFollowers",
                column: "UserId");

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
                name: "IX_ImageProducts_ImageId",
                table: "ImageProducts",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageProducts_SellerId",
                table: "ImageProducts",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArtistId",
                table: "Images",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTags_PikchaImageId",
                table: "ImageTags",
                column: "PikchaImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageViews_PikchaImageId",
                table: "ImageViews",
                column: "PikchaImageId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

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
                name: "ArtistFollowers");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "ImageProducts");

            migrationBuilder.DropTable(
                name: "ImageTags");

            migrationBuilder.DropTable(
                name: "ImageViews");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

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
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "PikchaRoles");

            migrationBuilder.DropTable(
                name: "PikchaUsers");
        }
    }
}
