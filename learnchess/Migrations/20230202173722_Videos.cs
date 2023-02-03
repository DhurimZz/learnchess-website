using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learnchess.Migrations
{
    public partial class Videos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "language",
                schema: "Identity",
                table: "Language",
                newName: "Language");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "Identity",
                table: "Language",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Videos",
                schema: "Identity",
                columns: table => new
                {
                    VideosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Video = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.VideosId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos",
                schema: "Identity");

            migrationBuilder.RenameColumn(
                name: "Language",
                schema: "Identity",
                table: "Language",
                newName: "language");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Identity",
                table: "Language",
                newName: "id");
        }
    }
}
