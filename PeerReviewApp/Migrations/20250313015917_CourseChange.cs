using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeerReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class CourseChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Institution_InstitutionId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstitutionId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Courses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstitutionId",
                table: "Courses",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Institution_InstitutionId",
                table: "Courses",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
