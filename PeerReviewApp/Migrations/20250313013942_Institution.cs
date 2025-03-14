using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeerReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class Institution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_UploaderId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_AspNetUsers_StudentId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_MemberId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_RevieweeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "InstutionId",
                table: "Courses",
                newName: "InstitutionId");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewerId",
                table: "Reviews",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RevieweeId",
                table: "Reviews",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "GroupMembers",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Grade",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UploaderId",
                table: "Document",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Courses",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AccountAge",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstitutionId",
                table: "Courses",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Institution_InstitutionId",
                table: "Courses",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_UploaderId",
                table: "Document",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_AspNetUsers_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_MemberId",
                table: "GroupMembers",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_RevieweeId",
                table: "Reviews",
                column: "RevieweeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Institution_InstitutionId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_UploaderId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_AspNetUsers_StudentId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_MemberId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_RevieweeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstitutionId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "InstitutionId",
                table: "Courses",
                newName: "InstutionId");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "ReviewerId",
                keyValue: null,
                column: "ReviewerId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewerId",
                table: "Reviews",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "RevieweeId",
                keyValue: null,
                column: "RevieweeId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RevieweeId",
                table: "Reviews",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "GroupMembers",
                keyColumn: "MemberId",
                keyValue: null,
                column: "MemberId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "GroupMembers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "StudentId",
                keyValue: null,
                column: "StudentId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Grade",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Document",
                keyColumn: "UploaderId",
                keyValue: null,
                column: "UploaderId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UploaderId",
                table: "Document",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "InstructorId",
                keyValue: null,
                column: "InstructorId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Courses",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AccountAge",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_UploaderId",
                table: "Document",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_AspNetUsers_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_MemberId",
                table: "GroupMembers",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_RevieweeId",
                table: "Reviews",
                column: "RevieweeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewerId",
                table: "Reviews",
                column: "ReviewerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
