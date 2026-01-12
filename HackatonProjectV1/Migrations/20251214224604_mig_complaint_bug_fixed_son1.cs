using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackatonProjectV1.Migrations
{
    /// <inheritdoc />
    public partial class mig_complaint_bug_fixed_son1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "complaints",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "complaints");
        }
    }
}
