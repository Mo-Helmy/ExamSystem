using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeFk_ExamQuestions_Answer_To_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "ExamsQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions",
                column: "AnswerId",
                unique: true,
                filter: "[AnswerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "ExamsQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamsQuestions_AnswerId",
                table: "ExamsQuestions",
                column: "AnswerId",
                unique: true);
        }
    }
}
