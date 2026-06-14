using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudSecOps.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerAssignmentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedAnalyst",
                table: "IncidentReports",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedAt",
                table: "IncidentReports",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerNote",
                table: "IncidentReports",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedAnalyst",
                table: "IncidentReports");

            migrationBuilder.DropColumn(
                name: "AssignedAt",
                table: "IncidentReports");

            migrationBuilder.DropColumn(
                name: "ManagerNote",
                table: "IncidentReports");
        }
    }
}
