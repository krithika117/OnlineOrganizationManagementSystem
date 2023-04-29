using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrganizationManagementSystem.Migrations
{
    public partial class allchangesAddCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Drop<string>(
            //    name: "Type",
            //    table: "CalendarEvent",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CalendarEvent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "CalendarEvent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
