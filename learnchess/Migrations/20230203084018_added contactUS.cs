using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learnchess.Migrations
{
    public partial class addedcontactUS : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
