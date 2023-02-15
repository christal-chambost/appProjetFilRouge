using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkedQuestionAndQuestionAnswerEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionAnswerId",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_questions_QuestionAnswerId",
                table: "questions",
                column: "QuestionAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questionAnswers_QuestionAnswerId",
                table: "questions",
                column: "QuestionAnswerId",
                principalTable: "questionAnswers",
                principalColumn: "questionanswer_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
