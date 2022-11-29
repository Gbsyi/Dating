using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dating.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Profiles");

            migrationBuilder.AddColumn<Guid>(
                name: "GenderFk",
                table: "Profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Пол пользователя");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_GenderFk",
                table: "Profiles",
                column: "GenderFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Genders_GenderFk",
                table: "Profiles",
                column: "GenderFk",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Genders_GenderFk",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_GenderFk",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "GenderFk",
                table: "Profiles");

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "Profiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Пол пользователя");
        }
    }
}
