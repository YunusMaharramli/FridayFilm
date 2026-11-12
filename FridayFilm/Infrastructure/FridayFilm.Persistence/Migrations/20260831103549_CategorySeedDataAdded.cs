using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategorySeedDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "Slug", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Aksiya", "aksiya", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Komediya", "komediya", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Dram", "dram", null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Qorxu", "qorxu", null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Elmi Fantastika", "elmi-fantastika", null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Romantika", "romantika", null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Triller", "triller", null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Sənədli", "senedli", null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Fantastika", "fantastika", null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Animasiya", "animasiya", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Müəmma", "muemma", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Macəra", "macera", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Cinayət", "cinayet", null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Ailə", "aile", null },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Utc), false, "Tarixi", "tarixi", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));
        }
    }
}
