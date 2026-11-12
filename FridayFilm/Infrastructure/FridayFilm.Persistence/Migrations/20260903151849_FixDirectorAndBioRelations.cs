using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDirectorAndBioRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmImages_Directors_DirectorId",
                table: "FilmImages");

            migrationBuilder.DropIndex(
                name: "IX_FilmImages_DirectorId",
                table: "FilmImages");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "FilmImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DirectorId",
                table: "FilmImages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmImages_DirectorId",
                table: "FilmImages",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmImages_Directors_DirectorId",
                table: "FilmImages",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id");
        }
    }
}
