using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.TicketBooking.DAO.Migrations
{
    public partial class AddMoneymanagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_Orders_OrderId",
                table: "Attendees");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SeatRanks_SeatRankId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Events_EventId",
                table: "StaticFiles");

            migrationBuilder.DropTable(
                name: "SurveyResponseDetails");

            migrationBuilder.DropTable(
                name: "SurveyQuestionDetails");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SeatRankId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QR",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SeatRankId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Attendees",
                newName: "OrderDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendees_OrderId",
                table: "Attendees",
                newName: "IX_Attendees_OrderDetailId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "StaticFiles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "StaticFiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MoneySponsorAmount",
                table: "EventSponsors",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SponsorDescription",
                table: "EventSponsors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SponsorType",
                table: "EventSponsors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QR",
                table: "Attendees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SeatRankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_OrderDetails_SeatRanks_SeatRankId",
                        column: x => x.SeatRankId,
                        principalTable: "SeatRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SponsorMoneyHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    IsFromSponsor = table.Column<bool>(type: "bit", nullable: false),
                    EventSponsorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorMoneyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SponsorMoneyHistories_EventSponsors_EventSponsorId",
                        column: x => x.EventSponsorId,
                        principalTable: "EventSponsors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonInChargeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "888f3f00-16de-47ee-bde9-c77e4541a645", "888f3f00-16de-47ee-bde9-c77e4541a645", "PM", "pm" });

            migrationBuilder.CreateIndex(
                name: "IX_StaticFiles_PostId",
                table: "StaticFiles",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SeatRankId",
                table: "OrderDetails",
                column: "SeatRankId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_EventId",
                table: "Posts",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SponsorMoneyHistories_EventSponsorId",
                table: "SponsorMoneyHistories",
                column: "EventSponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EventId",
                table: "Tasks",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_OrderDetails_OrderDetailId",
                table: "Attendees",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Events_EventId",
                table: "StaticFiles",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticFiles_Posts_PostId",
                table: "StaticFiles",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendees_OrderDetails_OrderDetailId",
                table: "Attendees");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Events_EventId",
                table: "StaticFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StaticFiles_Posts_PostId",
                table: "StaticFiles");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "SponsorMoneyHistories");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_StaticFiles_PostId",
                table: "StaticFiles");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "888f3f00-16de-47ee-bde9-c77e4541a645");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "StaticFiles");

            migrationBuilder.DropColumn(
                name: "MoneySponsorAmount",
                table: "EventSponsors");

            migrationBuilder.DropColumn(
                name: "SponsorDescription",
                table: "EventSponsors");

            migrationBuilder.DropColumn(
                name: "SponsorType",
                table: "EventSponsors");

            migrationBuilder.DropColumn(
                name: "QR",
                table: "Attendees");

            migrationBuilder.RenameColumn(
                name: "OrderDetailId",
                table: "Attendees",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendees_OrderDetailId",
                table: "Attendees",
                newName: "IX_Attendees_OrderId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "StaticFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.CreateTable(
                name: "SurveyQuestionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerType = table.Column<int>(type: "int", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatingMax = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionDetails_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResponseDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SurveyQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    TextAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyResponseDetails_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SurveyResponseDetails_SurveyQuestionDetails_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestionDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SeatRankId",
                table: "Orders",
                column: "SeatRankId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionDetails_SurveyId",
                table: "SurveyQuestionDetails",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponseDetails_AccountId",
                table: "SurveyResponseDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponseDetails_SurveyQuestionId",
                table: "SurveyResponseDetails",
                column: "SurveyQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendees_Orders_OrderId",
                table: "Attendees",
                column: "OrderId",
                principalTable: "Orders",
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
                name: "FK_StaticFiles_Events_EventId",
                table: "StaticFiles",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
