using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackatonProjectV1.Migrations
{
    /// <inheritdoc />
    public partial class mig_page_entites3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_faculties_FacultyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsertId",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "DepartmantId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DepartmenId",
                table: "contents",
                newName: "departmentId");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "AspNetUsers",
                newName: "facultyId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_FacultyId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_facultyId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "comments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_faculties_facultyId",
                table: "AspNetUsers",
                column: "facultyId",
                principalTable: "faculties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_faculties_facultyId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "departmentId",
                table: "contents",
                newName: "DepartmenId");

            migrationBuilder.RenameColumn(
                name: "facultyId",
                table: "AspNetUsers",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_facultyId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_FacultyId");

            migrationBuilder.AddColumn<string>(
                name: "UsertId",
                table: "contents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmantId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_faculties_FacultyId",
                table: "AspNetUsers",
                column: "FacultyId",
                principalTable: "faculties",
                principalColumn: "Id");
        }
    }
}
