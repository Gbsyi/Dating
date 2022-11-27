using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dating.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Configurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                comment: "Логин пользователя",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                comment: "Пароль пользователя",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "Profiles",
                type: "integer",
                nullable: false,
                comment: "Пол пользователя",
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Profiles",
                type: "text",
                nullable: false,
                comment: "Имя пользователя",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Profiles",
                type: "text",
                nullable: false,
                comment: "Описание пользователя",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Profiles",
                type: "integer",
                nullable: false,
                comment: "Возраст пользователя",
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genders",
                type: "text",
                nullable: false,
                comment: "Имя гендера",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrientations_GenderFk",
                table: "UserOrientations",
                column: "GenderFk");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrientations_UserFk",
                table: "UserOrientations",
                column: "UserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_ChatFk",
                table: "Pairs",
                column: "ChatFk");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_MatchedUserFk",
                table: "Pairs",
                column: "MatchedUserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_UserFk",
                table: "Pairs",
                column: "UserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatFk",
                table: "Messages",
                column: "ChatFk");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserFk",
                table: "Messages",
                column: "UserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PairFk",
                table: "Likes",
                column: "PairFk");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserFk",
                table: "Likes",
                column: "UserFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Pairs_PairFk",
                table: "Likes",
                column: "PairFk",
                principalTable: "Pairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserFk",
                table: "Likes",
                column: "UserFk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatFk",
                table: "Messages",
                column: "ChatFk",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserFk",
                table: "Messages",
                column: "UserFk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Chats_ChatFk",
                table: "Pairs",
                column: "ChatFk",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Users_MatchedUserFk",
                table: "Pairs",
                column: "MatchedUserFk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Users_UserFk",
                table: "Pairs",
                column: "UserFk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Users_Id",
                table: "Profiles",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrientations_Genders_GenderFk",
                table: "UserOrientations",
                column: "GenderFk",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrientations_Users_UserFk",
                table: "UserOrientations",
                column: "UserFk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Pairs_PairFk",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserFk",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatFk",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserFk",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Chats_ChatFk",
                table: "Pairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Users_MatchedUserFk",
                table: "Pairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Users_UserFk",
                table: "Pairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_Id",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrientations_Genders_GenderFk",
                table: "UserOrientations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrientations_Users_UserFk",
                table: "UserOrientations");

            migrationBuilder.DropIndex(
                name: "IX_UserOrientations_GenderFk",
                table: "UserOrientations");

            migrationBuilder.DropIndex(
                name: "IX_UserOrientations_UserFk",
                table: "UserOrientations");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_ChatFk",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_MatchedUserFk",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_UserFk",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatFk",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserFk",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Likes_PairFk",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserFk",
                table: "Likes");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Логин пользователя");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Пароль пользователя");

            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "Profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Пол пользователя");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Profiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Имя пользователя");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Profiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Описание пользователя");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Возраст пользователя");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Имя гендера");
        }
    }
}
