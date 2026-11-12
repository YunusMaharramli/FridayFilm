using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixActorImageRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmImages_Actors_ActorId",
                table: "FilmImages");

            migrationBuilder.DropIndex(
                name: "IX_FilmImages_ActorId",
                table: "FilmImages");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "FilmImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActorId",
                table: "FilmImages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmImages_ActorId",
                table: "FilmImages",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmImages_Actors_ActorId",
                table: "FilmImages",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");
        }
    }
}
