using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeerReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class reviewUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Reviews",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Reviews",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Reviews");
        }
    }
}
