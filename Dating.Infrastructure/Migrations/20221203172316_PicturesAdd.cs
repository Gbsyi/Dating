using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dating.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PicturesAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PictureFk",
                table: "Profiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PictureFk",
                table: "Profiles",
                column: "PictureFk",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Pictures_PictureFk",
                table: "Profiles",
                column: "PictureFk",
                principalTable: "Pictures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Pictures_PictureFk",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_PictureFk",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PictureFk",
                table: "Profiles");
        }
    }
}
