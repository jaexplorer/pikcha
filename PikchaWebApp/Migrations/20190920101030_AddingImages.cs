using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PikchaWebApp.Migrations
{
    public partial class AddingImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "PikchaImageTags",
                columns: table => new
                {
                    PikchaImageTagId = table.Column<long>(nullable: false),
                    ImageTagId = table.Column<long>(nullable: false),
                    PikchaImageId = table.Column<long>(nullable: false),
                    PikchaImageId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PikchaImageTags", x => x.PikchaImageTagId);
                    table.ForeignKey(
                        name: "FK_PikchaImageTags_ImageTags_ImageTagId",
                        column: x => x.ImageTagId,
                        principalTable: "ImageTags",
                        principalColumn: "ImageTagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PikchaImageTags_PikchaImages_PikchaImageId1",
                        column: x => x.PikchaImageId1,
                        principalTable: "PikchaImages",
                        principalColumn: "PikchaImageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PikchaImages_ArtistId",
                table: "PikchaImages",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaImageTags_ImageTagId",
                table: "PikchaImageTags",
                column: "ImageTagId");

            migrationBuilder.CreateIndex(
                name: "IX_PikchaImageTags_PikchaImageId1",
                table: "PikchaImageTags",
                column: "PikchaImageId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PikchaImageTags");

            migrationBuilder.DropTable(
                name: "ImageTags");

            migrationBuilder.DropTable(
                name: "PikchaImages");
        }
    }
}
