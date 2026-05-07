using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudSecOps.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddDraftSubmissionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedAt",
                table: "IncidentReports",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedAt",
                table: "IncidentReports");
        }
    }
}
