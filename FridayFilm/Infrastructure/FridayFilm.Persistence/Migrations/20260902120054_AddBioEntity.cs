using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBioEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ContactPhone = table.Column<string>(type: "text", nullable: true),
                    ContactEmail = table.Column<string>(type: "text", nullable: true),
                    InstagramUrl = table.Column<string>(type: "text", nullable: true),
                    FacebookUrl = table.Column<string>(type: "text", nullable: true),
                    TwitterUrl = table.Column<string>(type: "text", nullable: true),
                    LogoId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bios_FilmImages_LogoId",
                        column: x => x.LogoId,
                        principalTable: "FilmImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Bios",
                columns: new[] { "Id", "ContactEmail", "ContactPhone", "CreatedDate", "Description", "FacebookUrl", "InstagramUrl", "IsDeleted", "LogoId", "TwitterUrl", "UpdatedDate" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-123456789abc"), "info@fridayfilm.com", "+994 50 123 45 67", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FridayFilm - Dünyanın ən yaxşı filmlərini kəşf etmək üçün ideal platforma.", "https://facebook.com/fridayfilm", "https://instagram.com/fridayfilm", false, null, "https://twitter.com/fridayfilm", null });

            migrationBuilder.CreateIndex(
                name: "IX_Bios_LogoId",
                table: "Bios",
                column: "LogoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bios");
        }
    }
}
