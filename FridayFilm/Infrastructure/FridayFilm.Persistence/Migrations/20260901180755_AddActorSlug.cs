using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActorSlug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Actors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555501"),
                column: "Slug",
                value: "leonardo-dicaprio");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555502"),
                column: "Slug",
                value: "scarlett-johansson");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555503"),
                column: "Slug",
                value: "cillian-murphy");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555504"),
                column: "Slug",
                value: "margot-robbie");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555505"),
                column: "Slug",
                value: "tom-hardy");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555506"),
                column: "Slug",
                value: "meryl-streep");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555507"),
                column: "Slug",
                value: "keanu-reeves");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555508"),
                column: "Slug",
                value: "natalie-portman");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555509"),
                column: "Slug",
                value: "christian-bale");

            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555510"),
                column: "Slug",
                value: "charlize-theron");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Slug",
                table: "Actors",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Actors_Slug",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Actors");
        }
    }
}
