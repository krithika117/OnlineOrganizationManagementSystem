using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrganizationManagementSystem.Migrations
{
    public partial class taskscreationManagerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ManagerId",
                table: "Tasks",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_ManagerId",
                table: "Tasks",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_ManagerId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ManagerId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Tasks");
        }
    }
}
