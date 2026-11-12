using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageAndMovieDetailRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmImages_Movies_MovieId",
                table: "FilmImages");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "FilmImages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Directors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Actors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Directors_ImageId",
                table: "Directors",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ImageId",
                table: "Actors",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_FilmImages_ImageId",
                table: "Actors",
                column: "ImageId",
                principalTable: "FilmImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Directors_FilmImages_ImageId",
                table: "Directors",
                column: "ImageId",
                principalTable: "FilmImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmImages_Movies_MovieId",
                table: "FilmImages",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_FilmImages_ImageId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Directors_FilmImages_ImageId",
                table: "Directors");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmImages_Movies_MovieId",
                table: "FilmImages");

            migrationBuilder.DropIndex(
                name: "IX_Directors_ImageId",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_ImageId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Actors");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovieId",
                table: "FilmImages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmImages_Movies_MovieId",
                table: "FilmImages",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
