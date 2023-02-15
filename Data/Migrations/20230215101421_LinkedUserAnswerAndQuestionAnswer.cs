using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkedUserAnswerAndQuestionAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionAnswerUserAnswer",
                columns: table => new
                {
                    QuestionAnswersQuestionAnswerId = table.Column<int>(type: "int", nullable: false),
                    UserAnswersUserAnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerUserAnswer", x => new { x.QuestionAnswersQuestionAnswerId, x.UserAnswersUserAnswerId });
                    table.ForeignKey(
                        name: "FK_QuestionAnswerUserAnswer_questionAnswers_QuestionAnswersQuestionAnswerId",
                        column: x => x.QuestionAnswersQuestionAnswerId,
                        principalTable: "questionAnswers",
                        principalColumn: "questionanswer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerUserAnswer_userAnswer_UserAnswersUserAnswerId",
                        column: x => x.UserAnswersUserAnswerId,
                        principalTable: "userAnswer",
                        principalColumn: "userAnswer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerUserAnswer_UserAnswersUserAnswerId",
                table: "QuestionAnswerUserAnswer",
                column: "UserAnswersUserAnswerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswerUserAnswer");
        }
    }
}
