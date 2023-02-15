using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkedQuizAndQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionTypeId",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_questions_QuestionTypeId",
                table: "questions",
                column: "QuestionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questionTypes_QuestionTypeId",
                table: "questions",
                column: "QuestionTypeId",
                principalTable: "questionTypes",
                principalColumn: "questiontype_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_questionTypes_QuestionTypeId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_QuestionTypeId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "QuestionTypeId",
                table: "questions");
        }
    }
}
