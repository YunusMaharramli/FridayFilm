using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateActorImageAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Actors",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Actors",
                type: "integer",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "CreatedDate", "FullName", "Gender", "ImageId", "IsDeleted", "Nationality", "Nickname", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555501"), "Academy Award-winning actor known for Titanic, Inception, and The Revenant.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leonardo DiCaprio", 1, null, false, "American", "Leo", null },
                    { new Guid("11111111-2222-3333-4444-555555555502"), "Highly paid actress globally, known for her role as Black Widow in the MCU.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scarlett Johansson", 2, null, false, "American", "ScarJo", null },
                    { new Guid("11111111-2222-3333-4444-555555555503"), "Acclaimed for his roles in Peaky Blinders and Christopher Nolan's Oppenheimer.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cillian Murphy", 1, null, false, "Irish", "Tommy", null },
                    { new Guid("11111111-2222-3333-4444-555555555504"), "Known for blockbuster hits like The Wolf of Wall Street and Barbie.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Margot Robbie", 2, null, false, "Australian", "Magot", null },
                    { new Guid("11111111-2222-3333-4444-555555555505"), "Versatile actor famous for Mad Max: Fury Road, Venom, and The Dark Knight Rises.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tom Hardy", 1, null, false, "British", null, null },
                    { new Guid("11111111-2222-3333-4444-555555555506"), "Often described as the best actress of her generation, holding a record number of Academy Award nominations.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meryl Streep", 2, null, false, "American", null, null },
                    { new Guid("11111111-2222-3333-4444-555555555507"), "Beloved action star of The Matrix and John Wick franchises.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Keanu Reeves", 1, null, false, "Canadian", "The One", null },
                    { new Guid("11111111-2222-3333-4444-555555555508"), "Oscar winner for Black Swan and famous for her role in Star Wars.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Natalie Portman", 2, null, false, "Israeli/American", "Nat", null },
                    { new Guid("11111111-2222-3333-4444-555555555509"), "Known for his intense method acting and physical transformations for roles.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christian Bale", 1, null, false, "British", null, null },
                    { new Guid("11111111-2222-3333-4444-555555555510"), "Critically acclaimed star of Monster and Mad Max: Fury Road.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charlize Theron", 2, null, false, "South African", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555501"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555502"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555503"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555504"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555505"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555506"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555507"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555508"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555509"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-2222-3333-4444-555555555510"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Actors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Actors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 3);
        }
    }
}
