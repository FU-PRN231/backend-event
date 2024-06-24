using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.TicketBooking.DAO.Migrations
{
    public partial class RemoveAbundantStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetailId",
                table: "StaticFiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticFiles_OrderDetailId",
                table: "StaticFiles",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_OrderDetails_OrderDetailId",
                table: "StaticFiles",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_OrderDetails_OrderDetailId",
                table: "StaticFiles");

            migrationBuilder.DropIndex(
                name: "IX_StaticFiles_OrderDetailId",
                table: "StaticFiles");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "StaticFiles");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
