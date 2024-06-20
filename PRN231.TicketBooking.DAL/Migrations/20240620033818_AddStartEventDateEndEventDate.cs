using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.TicketBooking.DAO.Migrations
{
    public partial class AddStartEventDateEndEventDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndEventDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartEventDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndEventDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartEventDate",
                table: "Events");
        }
    }
}
