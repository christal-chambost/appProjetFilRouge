using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndIsCorrectToUserAnswerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "userAnswer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "userAnswer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_userAnswer_ApplicationUserId",
                table: "userAnswer",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userAnswer_AspNetUsers_ApplicationUserId",
                table: "userAnswer",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userAnswer_AspNetUsers_ApplicationUserId",
                table: "userAnswer");

            migrationBuilder.DropIndex(
                name: "IX_userAnswer_ApplicationUserId",
                table: "userAnswer");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "userAnswer");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "userAnswer");
        }
    }
}
