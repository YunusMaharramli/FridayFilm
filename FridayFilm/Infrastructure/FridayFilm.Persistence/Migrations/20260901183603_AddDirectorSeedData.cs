using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectorSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Bio", "CreatedDate", "FullName", "Gender", "ImageId", "IsDeleted", "Nationality", "Slug", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("22222222-3333-4444-5555-666666666601"), "Known for complex narratives like Inception, Interstellar, and Oppenheimer.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christopher Nolan", 1, null, false, "British-American", "christopher-nolan", null },
                    { new Guid("22222222-3333-4444-5555-666666666602"), "Famous for non-linear storylines and stylized violence in films like Pulp Fiction.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quentin Tarantino", 1, null, false, "American", "quentin-tarantino", null },
                    { new Guid("22222222-3333-4444-5555-666666666603"), "Acclaimed director of Lady Bird, Little Women, and Barbie.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greta Gerwig", 2, null, false, "American", "greta-gerwig", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666601"));

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666602"));

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-3333-4444-5555-666666666603"));
        }
    }
}
