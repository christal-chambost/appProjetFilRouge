using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppProjetFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNbQuestionsAndDateQuizzEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreation",
                table: "quizzes",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbQuestions",
                table: "quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "quizzes");

            migrationBuilder.DropColumn(
                name: "NbQuestions",
                table: "quizzes");
        }
    }
}
