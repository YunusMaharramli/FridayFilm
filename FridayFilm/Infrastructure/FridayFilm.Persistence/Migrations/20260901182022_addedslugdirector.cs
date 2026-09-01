using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridayFilm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedslugdirector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Directors",
                newName: "FullName");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Directors",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Directors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_Slug",
                table: "Directors",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Directors_Slug",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Directors");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Directors",
                newName: "Fullname");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Directors",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
