using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModificationUserAnswerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "userAnswer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "quizId",
                table: "userAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_userAnswer_quizId",
                table: "userAnswer",
                column: "quizId");

            migrationBuilder.AddForeignKey(
                name: "FK_userAnswer_quizzes_quizId",
                table: "userAnswer",
                column: "quizId",
                principalTable: "quizzes",
                principalColumn: "quiz_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userAnswer_quizzes_quizId",
                table: "userAnswer");

            migrationBuilder.DropIndex(
                name: "IX_userAnswer_quizId",
                table: "userAnswer");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "userAnswer");

            migrationBuilder.DropColumn(
                name: "quizId",
                table: "userAnswer");

        }
    }
}
