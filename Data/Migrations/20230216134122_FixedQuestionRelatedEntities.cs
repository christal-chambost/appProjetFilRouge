using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedQuestionRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_questionAnswers_QuestionAnswerId",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_questions_userAnswer_UserAnswerId",
                table: "questions");

            migrationBuilder.DropTable(
                name: "QuestionAnswerUserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_questions_UserAnswerId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "UserAnswerId",
                table: "questions");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "userAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionAnswerId",
                table: "questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CommentUser",
                table: "questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Correction",
                table: "questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_userAnswer_QuestionId",
                table: "userAnswer",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questionAnswers_QuestionAnswerId",
                table: "questions",
                column: "QuestionAnswerId",
                principalTable: "questionAnswers",
                principalColumn: "questionanswer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_userAnswer_questions_QuestionId",
                table: "userAnswer",
                column: "QuestionId",
                principalTable: "questions",
                principalColumn: "question_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_questionAnswers_QuestionAnswerId",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_userAnswer_questions_QuestionId",
                table: "userAnswer");

            migrationBuilder.DropIndex(
                name: "IX_userAnswer_QuestionId",
                table: "userAnswer");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "userAnswer");

            migrationBuilder.DropColumn(
                name: "CommentUser",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "Correction",
                table: "questions");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionAnswerId",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAnswerId",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_questions_UserAnswerId",
                table: "questions",
                column: "UserAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerUserAnswer_UserAnswersUserAnswerId",
                table: "QuestionAnswerUserAnswer",
                column: "UserAnswersUserAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questionAnswers_QuestionAnswerId",
                table: "questions",
                column: "QuestionAnswerId",
                principalTable: "questionAnswers",
                principalColumn: "questionanswer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_questions_userAnswer_UserAnswerId",
                table: "questions",
                column: "UserAnswerId",
                principalTable: "userAnswer",
                principalColumn: "userAnswer_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
