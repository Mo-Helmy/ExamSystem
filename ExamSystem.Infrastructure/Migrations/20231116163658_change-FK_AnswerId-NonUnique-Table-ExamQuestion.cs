using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeFK_AnswerIdNonUniqueTableExamQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions");

            migrationBuilder.CreateTable(
                name: "ExamOverViews",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRight = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestionWithAnswerStoredProcedures",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    AnswerBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions",
                column: "AnswerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamOverViews");

            migrationBuilder.DropTable(
                name: "ExamQuestionWithAnswerStoredProcedures");

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
