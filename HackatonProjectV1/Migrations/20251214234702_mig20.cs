using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackatonProjectV1.Migrations
{
    /// <inheritdoc />
    public partial class mig20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplainId",
                table: "comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplainId",
                table: "comments",
                type: "int",
                nullable: true);
        }
    }
}
