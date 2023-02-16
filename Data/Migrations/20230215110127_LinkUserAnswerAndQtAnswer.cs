using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkUserAnswerAndQtAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "quizzes",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserAnswerId",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_questions_UserAnswerId",
                table: "questions",
                column: "UserAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_userAnswer_UserAnswerId",
                table: "questions",
                column: "UserAnswerId",
                principalTable: "userAnswer",
                principalColumn: "userAnswer_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_userAnswer_UserAnswerId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_UserAnswerId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "name",
                table: "quizzes");

            migrationBuilder.DropColumn(
                name: "UserAnswerId",
                table: "questions");
        }
    }
}
