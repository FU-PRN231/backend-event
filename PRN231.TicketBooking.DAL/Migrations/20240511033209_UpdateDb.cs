using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.TicketBooking.DAO.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventS_AspNetUsers_CreateBy",
                table: "EventS");

            migrationBuilder.DropForeignKey(
                name: "FK_EventS_AspNetUsers_UpdateBy",
                table: "EventS");

            migrationBuilder.DropForeignKey(
                name: "FK_EventS_Locations_LocationId",
                table: "EventS");

            migrationBuilder.DropForeignKey(
                name: "FK_EventS_Organizations_OrganizationId",
                table: "EventS");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSponsors_EventS_EventId",
                table: "EventSponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatRanks_EventS_EventId",
                table: "SeatRanks");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_EventS_EventId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_EventS_EventId",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventS",
                table: "EventS");

            migrationBuilder.RenameTable(
                name: "EventS",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_EventS_UpdateBy",
                table: "Events",
                newName: "IX_Events_UpdateBy");

            migrationBuilder.RenameIndex(
                name: "IX_EventS_OrganizationId",
                table: "Events",
                newName: "IX_Events_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_EventS_LocationId",
                table: "Events",
                newName: "IX_Events_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_EventS_CreateBy",
                table: "Events",
                newName: "IX_Events_CreateBy");

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Surveys",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "Surveys",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "SurveyResponses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "SeatRanks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RemainingCapacity",
                table: "SeatRanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "SeatRanks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "QR",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeatRankId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckedIn = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendees_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Attendees_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CreateBy",
                table: "Surveys",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UpdateBy",
                table: "Surveys",
                column: "UpdateBy");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_AccountId",
                table: "SurveyResponses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SeatRankId",
                table: "Orders",
                column: "SeatRankId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_EventId",
                table: "Attendees",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_OrderId",
                table: "Attendees",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CreateBy",
                table: "Events",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_UpdateBy",
                table: "Events",
                column: "UpdateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Organizations_OrganizationId",
                table: "Events",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSponsors_Events_EventId",
                table: "EventSponsors",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SeatRanks_SeatRankId",
                table: "Orders",
                column: "SeatRankId",
                principalTable: "SeatRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatRanks_Events_EventId",
                table: "SeatRanks",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Events_EventId",
                table: "StaticFiles",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponses_AspNetUsers_AccountId",
                table: "SurveyResponses",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_AspNetUsers_CreateBy",
                table: "Surveys",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_AspNetUsers_UpdateBy",
                table: "Surveys",
                column: "UpdateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Events_EventId",
                table: "Surveys",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CreateBy",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_UpdateBy",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Organizations_OrganizationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSponsors_Events_EventId",
                table: "EventSponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SeatRanks_SeatRankId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatRanks_Events_EventId",
                table: "SeatRanks");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Events_EventId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponses_AspNetUsers_AccountId",
                table: "SurveyResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_AspNetUsers_CreateBy",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_AspNetUsers_UpdateBy",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Events_EventId",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_CreateBy",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_UpdateBy",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_SurveyResponses_AccountId",
                table: "SurveyResponses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SeatRankId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "SeatRanks");

            migrationBuilder.DropColumn(
                name: "RemainingCapacity",
                table: "SeatRanks");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "SeatRanks");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QR",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SeatRankId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "EventS");

            migrationBuilder.RenameIndex(
                name: "IX_Events_UpdateBy",
                table: "EventS",
                newName: "IX_EventS_UpdateBy");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganizationId",
                table: "EventS",
                newName: "IX_EventS_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_LocationId",
                table: "EventS",
                newName: "IX_EventS_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CreateBy",
                table: "EventS",
                newName: "IX_EventS_CreateBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventS",
                table: "EventS",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatRankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsTaken = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_SeatRanks_SeatRankId",
                        column: x => x.SeatRankId,
                        principalTable: "SeatRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SeatId",
                table: "OrderDetails",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatRankId",
                table: "Seats",
                column: "SeatRankId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventS_AspNetUsers_CreateBy",
                table: "EventS",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EventS_AspNetUsers_UpdateBy",
                table: "EventS",
                column: "UpdateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventS_Locations_LocationId",
                table: "EventS",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EventS_Organizations_OrganizationId",
                table: "EventS",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSponsors_EventS_EventId",
                table: "EventSponsors",
                column: "EventId",
                principalTable: "EventS",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatRanks_EventS_EventId",
                table: "SeatRanks",
                column: "EventId",
                principalTable: "EventS",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_EventS_EventId",
                table: "StaticFiles",
                column: "EventId",
                principalTable: "EventS",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_EventS_EventId",
                table: "Surveys",
                column: "EventId",
                principalTable: "EventS",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
