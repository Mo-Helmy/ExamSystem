using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateCertificateTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CertificateTopics_TopicId",
                table: "CertificateTopics");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateTopics_TopicId_CertificateId",
                table: "CertificateTopics",
                columns: new[] { "TopicId", "CertificateId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CertificateTopics_TopicId_CertificateId",
                table: "CertificateTopics");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateTopics_TopicId",
                table: "CertificateTopics",
                column: "TopicId");
        }
    }
}
