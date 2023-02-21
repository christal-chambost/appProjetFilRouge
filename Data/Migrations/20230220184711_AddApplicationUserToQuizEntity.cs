using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserToQuizEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "quizzes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_ApplicationUserId",
                table: "quizzes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_quizzes_AspNetUsers_ApplicationUserId",
                table: "quizzes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quizzes_AspNetUsers_ApplicationUserId",
                table: "quizzes");

            migrationBuilder.DropIndex(
                name: "IX_quizzes_ApplicationUserId",
                table: "quizzes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "quizzes");
        }
    }
}
