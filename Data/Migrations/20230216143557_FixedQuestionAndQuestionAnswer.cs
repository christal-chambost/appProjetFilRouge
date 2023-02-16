using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedQuestionAndQuestionAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_questionAnswers_QuestionAnswerId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_QuestionAnswerId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "QuestionAnswerId",
                table: "questions");

            migrationBuilder.AddColumn<int>(
                name: "Questionid",
                table: "questionAnswers",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_questionAnswers_Questionid",
                table: "questionAnswers",
                column: "Questionid");

            migrationBuilder.AddForeignKey(
                name: "FK_questionAnswers_questions_Questionid",
                table: "questionAnswers",
                column: "Questionid",
                principalTable: "questions",
                principalColumn: "question_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questionAnswers_questions_Questionid",
                table: "questionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_questionAnswers_Questionid",
                table: "questionAnswers");

            migrationBuilder.DropColumn(
                name: "Questionid",
                table: "questionAnswers");

            migrationBuilder.AddColumn<int>(
                name: "QuestionAnswerId",
                table: "questions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_questions_QuestionAnswerId",
                table: "questions",
                column: "QuestionAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questionAnswers_QuestionAnswerId",
                table: "questions",
                column: "QuestionAnswerId",
                principalTable: "questionAnswers",
                principalColumn: "questionanswer_id");
        }
    }
}
