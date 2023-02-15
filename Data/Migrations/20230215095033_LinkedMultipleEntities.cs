using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkedMultipleEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnologyId",
                table: "questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_LevelId",
                table: "quizzes",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_LevelId",
                table: "questions",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_TechnologyId",
                table: "questions",
                column: "TechnologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_levels_LevelId",
                table: "questions",
                column: "LevelId",
                principalTable: "levels",
                principalColumn: "level_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_questions_technologies_TechnologyId",
                table: "questions",
                column: "TechnologyId",
                principalTable: "technologies",
                principalColumn: "technology_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quizzes_levels_LevelId",
                table: "quizzes",
                column: "LevelId",
                principalTable: "levels",
                principalColumn: "level_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_levels_LevelId",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_questions_technologies_TechnologyId",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_quizzes_levels_LevelId",
                table: "quizzes");

            migrationBuilder.DropIndex(
                name: "IX_quizzes_LevelId",
                table: "quizzes");

            migrationBuilder.DropIndex(
                name: "IX_questions_LevelId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_TechnologyId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "quizzes");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "TechnologyId",
                table: "questions");
        }
    }
}
