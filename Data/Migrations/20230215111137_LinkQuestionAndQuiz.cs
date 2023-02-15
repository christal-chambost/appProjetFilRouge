using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
	/// <inheritdoc />
	public partial class LinkQuestionAndQuiz : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "QuizId",
				table: "questions",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.CreateIndex(
				name: "IX_questions_QuizId",
				table: "questions",
				column: "QuizId");

			migrationBuilder.AddForeignKey(
				name: "FK_questions_quizzes_QuizId",
				table: "questions",
				column: "QuizId",
				principalTable: "quizzes",
				principalColumn: "quiz_id",
				onDelete: ReferentialAction.NoAction,
				onUpdate: ReferentialAction.NoAction);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_questions_quizzes_QuizId",
				table: "questions");

			migrationBuilder.DropIndex(
				name: "IX_questions_QuizId",
				table: "questions");

			migrationBuilder.DropColumn(
				name: "QuizId",
				table: "questions");
		}
	}
}
