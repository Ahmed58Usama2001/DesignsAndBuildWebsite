using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignsAndBuild.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateOurProjectTableAndImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsSeened",
                table: "CustomerMessages",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ClientProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImags_ClientProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ClientProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImags_ProjectId",
                table: "ProjectImags",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectImags");

            migrationBuilder.DropTable(
                name: "ClientProjects");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSeened",
                table: "CustomerMessages",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
