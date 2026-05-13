using System;
using CloudSecOps.Web.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudSecOps.Web.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260511000000_AddIncidentReportDraftSubmissionFields")]
public partial class AddIncidentReportDraftSubmissionFields : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "SubmittedAt",
            table: "IncidentReports",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Title",
            table: "IncidentReports",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "IncidentReports",
            type: "character varying(1000)",
            maxLength: 1000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<string>(
            name: "AffectedAsset",
            table: "IncidentReports",
            type: "character varying(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Title",
            table: "IncidentReports",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "IncidentReports",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(1000)",
            oldMaxLength: 1000);

        migrationBuilder.AlterColumn<string>(
            name: "AffectedAsset",
            table: "IncidentReports",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100)",
            oldMaxLength: 100);

        migrationBuilder.DropColumn(
            name: "SubmittedAt",
            table: "IncidentReports");
    }
}
