using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackatonProjectV1.Migrations
{
    /// <inheritdoc />
    public partial class mig_complaint_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundImagePath",
                table: "universities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoImagePath",
                table: "universities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "StarCount",
                table: "universities",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComplaintId",
                table: "comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Support = table.Column<int>(type: "int", nullable: true),
                    Dislike = table.Column<int>(type: "int", nullable: true),
                    Label = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: true),
                    FacultyId = table.Column<int>(type: "int", nullable: true),
                    departmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_complaints_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_complaints_departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_complaints_faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "faculties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_complaints_universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "universities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_ComplaintId",
                table: "comments",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_departmentId",
                table: "complaints",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_FacultyId",
                table: "complaints",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_UniversityId",
                table: "complaints",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_UserId",
                table: "complaints",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_complaints_ComplaintId",
                table: "comments",
                column: "ComplaintId",
                principalTable: "complaints",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_complaints_ComplaintId",
                table: "comments");

            migrationBuilder.DropTable(
                name: "complaints");

            migrationBuilder.DropIndex(
                name: "IX_comments_ComplaintId",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "BackgroundImagePath",
                table: "universities");

            migrationBuilder.DropColumn(
                name: "LogoImagePath",
                table: "universities");

            migrationBuilder.DropColumn(
                name: "StarCount",
                table: "universities");

            migrationBuilder.DropColumn(
                name: "ComplaintId",
                table: "comments");
        }
    }
}
