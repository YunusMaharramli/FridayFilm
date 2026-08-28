using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Directors",
                newName: "Gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Directors",
                newName: "Genre");
        }
    }
}
