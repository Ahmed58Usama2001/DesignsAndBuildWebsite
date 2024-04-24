using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesignsAndBuild.Repository.Migrations
{
    /// <inheritdoc />
    public partial class handleDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "OurProjects");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "OurProjects");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "OurProjects",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
