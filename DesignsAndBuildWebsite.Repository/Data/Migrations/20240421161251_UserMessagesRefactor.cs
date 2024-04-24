using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignsAndBuild.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UserMessagesRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImags_OurProjects_OurProjectId",
                table: "ProjectImags");

            migrationBuilder.DropTable(
                name: "CustomerMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectImags",
                table: "ProjectImags");

            migrationBuilder.RenameTable(
                name: "ProjectImags",
                newName: "ProjectImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectImags_OurProjectId",
                table: "ProjectImages",
                newName: "IX_ProjectImages_OurProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectImages",
                table: "ProjectImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    SeenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImages_OurProjects_OurProjectId",
                table: "ProjectImages",
                column: "OurProjectId",
                principalTable: "OurProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImages_OurProjects_OurProjectId",
                table: "ProjectImages");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectImages",
                table: "ProjectImages");

            migrationBuilder.RenameTable(
                name: "ProjectImages",
                newName: "ProjectImags");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectImages_OurProjectId",
                table: "ProjectImags",
                newName: "IX_ProjectImags_OurProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectImags",
                table: "ProjectImags",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CustomerMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateMessageSeenAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSeened = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeenByWho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendMessageDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMessages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImags_OurProjects_OurProjectId",
                table: "ProjectImags",
                column: "OurProjectId",
                principalTable: "OurProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
