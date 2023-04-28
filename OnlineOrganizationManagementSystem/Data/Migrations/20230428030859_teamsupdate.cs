using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrganizationManagementSystem.Data.Migrations
{
    public partial class teamsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_Users",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Users",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Users",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "BackendDeveloper",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FrontendDeveloper",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportsTo",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeamLead",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tester",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UIUXDeveloper",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_ManagerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "BackendDeveloper",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "FrontendDeveloper",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ReportsTo",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamLead",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Tester",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UIUXDeveloper",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "Users",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Users",
                table: "AspNetUsers",
                column: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_Users",
                table: "AspNetUsers",
                column: "Users",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
