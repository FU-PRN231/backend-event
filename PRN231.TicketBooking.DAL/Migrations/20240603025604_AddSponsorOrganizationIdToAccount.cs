using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.TicketBooking.DAO.Migrations
{
    public partial class AddSponsorOrganizationIdToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Events_EventId",
                table: "Attendees");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_AspNetUsers_CreateBy",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_AspNetUsers_UpdateBy",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_AspNetUsers_AccountId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_AccountId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_CreateBy",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_UpdateBy",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Attendees_EventId",
                table: "Attendees");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Attendees");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SponsorId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrganizationId",
                table: "AspNetUsers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SponsorId",
                table: "AspNetUsers",
                column: "SponsorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationId",
                table: "AspNetUsers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sponsors_SponsorId",
                table: "AspNetUsers",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sponsors_SponsorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OrganizationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SponsorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SponsorId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Sponsors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Organizations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "Organizations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Organizations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Attendees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_AccountId",
                table: "Sponsors",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CreateBy",
                table: "Organizations",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UpdateBy",
                table: "Organizations",
                column: "UpdateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_EventId",
                table: "Attendees",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Events_EventId",
                table: "Attendees",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_AspNetUsers_CreateBy",
                table: "Organizations",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_AspNetUsers_UpdateBy",
                table: "Organizations",
                column: "UpdateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_AspNetUsers_AccountId",
                table: "Sponsors",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
