using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignsAndBuild.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOurProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "OurProjects");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "OurProjects");

            migrationBuilder.AddColumn<string>(
                name: "ArabicLocation",
                table: "OurProjects",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "OurProjects",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "OurProjects",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicLocation",
                table: "OurProjects");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "OurProjects");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "OurProjects");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "OurProjects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "OurProjects",
                type: "datetime2",
                nullable: true);
        }
    }
}
