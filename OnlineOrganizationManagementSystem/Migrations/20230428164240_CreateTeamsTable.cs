using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrganizationManagementSystem.Migrations
{
    public partial class CreateTeamsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)",nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UIUXDeveloperId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FrontendDeveloperId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BackendDeveloperId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TesterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TeamLeadId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReportsToId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_BackendDeveloperId",
                        column: x => x.BackendDeveloperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_FrontendDeveloperId",
                        column: x => x.FrontendDeveloperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_ReporstToId",
                        column: x => x.ReportsToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_TeamLeadId",
                        column: x => x.TeamLeadId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_TesterId",
                        column: x => x.TesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_AspNetUsers_UIUXDeveloperId",
                        column: x => x.UIUXDeveloperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_BackendDeveloperId",
                table: "Teams",
                column: "BackendDeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FrontendDeveloperId",
                table: "Teams",
                column: "FrontendDeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ReporstToId",
                table: "Teams",
                column: "ReportsToId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamLeadId",
                table: "Teams",
                column: "TeamLeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TesterId",
                table: "Teams",
                column: "TesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UIUXDeveloperId",
                table: "Teams",
                column: "UIUXDeveloperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}