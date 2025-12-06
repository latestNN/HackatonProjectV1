using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackatonProjectV1.Migrations
{
    /// <inheritdoc />
    public partial class mig_user_prop_edited2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "AspNetUsers");
        }
    }
}
