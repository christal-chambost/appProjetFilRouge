using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedQuestionAnswerForQuestionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questionAnswers_questions_Questionid",
                table: "questionAnswers");

            migrationBuilder.RenameColumn(
                name: "Questionid",
                table: "questionAnswers",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_questionAnswers_Questionid",
                table: "questionAnswers",
                newName: "IX_questionAnswers_QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_questionAnswers_questions_QuestionId",
                table: "questionAnswers",
                column: "QuestionId",
                principalTable: "questions",
                principalColumn: "question_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questionAnswers_questions_QuestionId",
                table: "questionAnswers");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "questionAnswers",
                newName: "Questionid");

            migrationBuilder.RenameIndex(
                name: "IX_questionAnswers_QuestionId",
                table: "questionAnswers",
                newName: "IX_questionAnswers_Questionid");

            migrationBuilder.AddForeignKey(
                name: "FK_questionAnswers_questions_Questionid",
                table: "questionAnswers",
                column: "Questionid",
                principalTable: "questions",
                principalColumn: "question_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
