using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineOrganizationManagementSystem.Migrations
{
    public partial class removecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CalendarEvent");

            //migrationBuilder.RenameColumn(
            //    name: "Type",
            //    table: "CalendarEvent",
            //    newName: "EventType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "EventType",
            //    table: "CalendarEvent",
            //    newName: "Type");

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "CalendarEvent",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");
        }
    }
}
