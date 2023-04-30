using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrganizationManagementSystem.Migrations
{
    public partial class addedTeamIdCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Archives",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Archives");
        }
    }
}
