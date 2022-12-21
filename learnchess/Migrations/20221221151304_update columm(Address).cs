using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learnchess.Migrations
{
    /// <inheritdoc />
    public partial class updatecolummAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                schema: "Identity",
                table: "AspNetUsers",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "Identity",
                table: "AspNetUsers",
                newName: "Adress");
        }
    }
}
