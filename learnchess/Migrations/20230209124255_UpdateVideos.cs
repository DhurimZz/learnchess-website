using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learnchess.Migrations
{
    public partial class UpdateVideos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Videos",
                schema: "Identity",
                columns: table => new
                {
                    VideosId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Video = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LanguageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LevelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.VideosId);
                    table.ForeignKey(
                        name: "FK_Videos_authors_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "Identity",
                        principalTable: "authors",
                        principalColumn: "AuthorId");
                    table.ForeignKey(
                        name: "FK_Videos_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "Identity",
                        principalTable: "Language",
                        principalColumn: "LanguageId");
                    table.ForeignKey(
                        name: "FK_Videos_Levels_LevelId",
                        column: x => x.LevelId,
                        principalSchema: "Identity",
                        principalTable: "Levels",
                        principalColumn: "LevelId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_AuthorId",
                schema: "Identity",
                table: "Videos",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LanguageId",
                schema: "Identity",
                table: "Videos",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_LevelId",
                schema: "Identity",
                table: "Videos",
                column: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos",
                schema: "Identity");
        }
    }
}
