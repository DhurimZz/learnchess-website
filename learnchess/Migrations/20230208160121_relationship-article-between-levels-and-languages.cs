using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learnchess.Migrations
{
    public partial class relationshiparticlebetweenlevelsandlanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageId",
                schema: "Identity",
                table: "Article",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LevelId",
                schema: "Identity",
                table: "Article",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_LanguageId",
                schema: "Identity",
                table: "Article",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_LevelId",
                schema: "Identity",
                table: "Article",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Language_LanguageId",
                schema: "Identity",
                table: "Article",
                column: "LanguageId",
                principalSchema: "Identity",
                principalTable: "Language",
                principalColumn: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Levels_LevelId",
                schema: "Identity",
                table: "Article",
                column: "LevelId",
                principalSchema: "Identity",
                principalTable: "Levels",
                principalColumn: "LevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Language_LanguageId",
                schema: "Identity",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Levels_LevelId",
                schema: "Identity",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_LanguageId",
                schema: "Identity",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_LevelId",
                schema: "Identity",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                schema: "Identity",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "LevelId",
                schema: "Identity",
                table: "Article");
        }
    }
}
