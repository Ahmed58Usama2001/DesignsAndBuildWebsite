using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignsAndBuild.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendMessageDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSeened = table.Column<bool>(type: "bit", nullable: false),
                    DateMessageSeenAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeenByWho = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OurProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ArabicTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ArabicClientName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ArabicDescription = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OurProjectId = table.Column<int>(type: "int", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImags_OurProjects_OurProjectId",
                        column: x => x.OurProjectId,
                        principalTable: "OurProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImags_OurProjectId",
                table: "ProjectImags",
                column: "OurProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerMessages");

            migrationBuilder.DropTable(
                name: "ProjectImags");

            migrationBuilder.DropTable(
                name: "OurProjects");
        }
    }
}
