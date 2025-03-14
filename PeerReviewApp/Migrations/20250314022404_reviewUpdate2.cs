using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeerReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class reviewUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "Reviews",
                newName: "ParentAssignmentId");

            migrationBuilder.AddColumn<int>(
                name: "ParentCourseId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ParentAssignmentId",
                table: "Reviews",
                column: "ParentAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ParentCourseId",
                table: "Assignments",
                column: "ParentCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Courses_ParentCourseId",
                table: "Assignments",
                column: "ParentCourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Assignments_ParentAssignmentId",
                table: "Reviews",
                column: "ParentAssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Courses_ParentCourseId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Assignments_ParentAssignmentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ParentAssignmentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ParentCourseId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ParentCourseId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "ParentAssignmentId",
                table: "Reviews",
                newName: "AssignmentId");

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "Assignments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
