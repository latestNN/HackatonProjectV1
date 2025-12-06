using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackatonProjectV1.Migrations
{
    /// <inheritdoc />
    public partial class mig_page_entites2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contents_departments_departmentId",
                table: "contents");

            migrationBuilder.DropForeignKey(
                name: "FK_contents_faculties_FacultyId",
                table: "contents");

            migrationBuilder.DropForeignKey(
                name: "FK_contents_universities_UniversityId",
                table: "contents");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_faculties_FacultyId",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_faculties_universities_UniversityId",
                table: "faculties");

            migrationBuilder.DropIndex(
                name: "IX_contents_departmentId",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "departmentId",
                table: "contents");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "contents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_contents_departments_Id",
                table: "contents",
                column: "Id",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_contents_faculties_FacultyId",
                table: "contents",
                column: "FacultyId",
                principalTable: "faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_contents_universities_UniversityId",
                table: "contents",
                column: "UniversityId",
                principalTable: "universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_faculties_FacultyId",
                table: "departments",
                column: "FacultyId",
                principalTable: "faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_faculties_universities_UniversityId",
                table: "faculties",
                column: "UniversityId",
                principalTable: "universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contents_departments_Id",
                table: "contents");

            migrationBuilder.DropForeignKey(
                name: "FK_contents_faculties_FacultyId",
                table: "contents");

            migrationBuilder.DropForeignKey(
                name: "FK_contents_universities_UniversityId",
                table: "contents");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_faculties_FacultyId",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_faculties_universities_UniversityId",
                table: "faculties");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "contents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "departmentId",
                table: "contents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_contents_departmentId",
                table: "contents",
                column: "departmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_contents_departments_departmentId",
                table: "contents",
                column: "departmentId",
                principalTable: "departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_contents_faculties_FacultyId",
                table: "contents",
                column: "FacultyId",
                principalTable: "faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_contents_universities_UniversityId",
                table: "contents",
                column: "UniversityId",
                principalTable: "universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_faculties_FacultyId",
                table: "departments",
                column: "FacultyId",
                principalTable: "faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_faculties_universities_UniversityId",
                table: "faculties",
                column: "UniversityId",
                principalTable: "universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
