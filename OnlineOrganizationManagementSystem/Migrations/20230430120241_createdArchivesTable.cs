﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrganizationManagementSystem.Migrations
{
    public partial class createdArchivesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UIUXDeveloperId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontendDeveloperId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackendDeveloperId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TesterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamLeadId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportsToId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateArchived = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archives", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archives");
        }
    }
}