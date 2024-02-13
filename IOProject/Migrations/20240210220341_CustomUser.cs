using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOProject.Migrations
{
    /// <inheritdoc />
    public partial class CustomUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tags",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tags",
                table: "AspNetUsers");
        }
    }
}
