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
                name: "ProjectID",
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
                name: "ProjectID",
                table: "FileAttachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_HelpProjectId",
                table: "FileAttachments",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_FileAttachments_HelpProjects_HelpProjectId",
                table: "FileAttachments",
                column: "ProjectID",
                principalTable: "HelpProjects",
                principalColumn: "Id");
        }
    }
}
