using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dating.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LikesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Pairs_PairFk",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_PairFk",
                table: "Likes",
                column: "PairFk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_PairFk",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Pairs_PairFk",
                table: "Likes",
                column: "PairFk",
                principalTable: "Pairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
