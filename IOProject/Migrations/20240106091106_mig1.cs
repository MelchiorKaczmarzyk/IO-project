using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOProject.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileAttachments_HelpProjects_HelpProjectId",
                table: "FileAttachments");

            migrationBuilder.DropIndex(
                name: "IX_FileAttachments_HelpProjectId",
                table: "FileAttachments");

            migrationBuilder.DropColumn(
                name: "HelpProjectId",
                table: "FileAttachments");

            migrationBuilder.AddColumn<string>(
                name: "FileAttachments",
                table: "HelpProjects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileAttachments",
                table: "HelpProjects");

            migrationBuilder.AddColumn<int>(
                name: "HelpProjectId",
                table: "FileAttachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_HelpProjectId",
                table: "FileAttachments",
                column: "HelpProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_HelpProjects_HelpProjectId",
                table: "FileAttachments",
                column: "HelpProjectId",
                principalTable: "HelpProjects",
                principalColumn: "Id");
        }
    }
}
