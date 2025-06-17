using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRManagment1.Data.Migrations
{
    /// <inheritdoc />
    public partial class jobOpeningApplicant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobOpeningApplicant",
                columns: table => new
                {
                    JobOpeningApplicantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    JobOpeningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOpeningApplicant", x => x.JobOpeningApplicantId);
                    table.ForeignKey(
                        name: "FK_JobOpeningApplicant_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JobOpeningApplicant_JobOpening_JobOpeningId",
                        column: x => x.JobOpeningId,
                        principalTable: "JobOpening",
                        principalColumn: "JobOpeningId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOpeningApplicant_ApplicantId",
                table: "JobOpeningApplicant",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpeningApplicant_JobOpeningId",
                table: "JobOpeningApplicant",
                column: "JobOpeningId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOpeningApplicant");
        }
    }
}
