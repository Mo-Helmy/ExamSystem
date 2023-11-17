using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeFK_AnswerId_ExamQuestionstoNonUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions");

            migrationBuilder.CreateIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions",
                column: "AnswerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions");

            migrationBuilder.CreateIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions",
                column: "AnswerId",
                unique: true,
                filter: "[AnswerId] IS NOT NULL");
        }
    }
}
