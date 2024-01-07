using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IOProject.Migrations
{
    /// <inheritdoc />
    public partial class HelpProject0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HelpProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HelpProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAttachments_HelpProjects_HelpProjectId",
                        column: x => x.HelpProjectId,
                        principalTable: "HelpProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_HelpProjectId",
                table: "FileAttachments",
                column: "HelpProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "HelpProjects");
        }
    }
}
