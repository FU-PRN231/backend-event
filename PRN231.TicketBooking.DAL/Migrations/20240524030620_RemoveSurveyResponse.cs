using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.TicketBooking.DAO.Migrations
{
    public partial class RemoveSurveyResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_AspNetUsers_AccountId",
                table: "Sponsors");

            migrationBuilder.DropTable(
                name: "SurveyResponses");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "SurveyResponseDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Sponsors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponseDetails_AccountId",
                table: "SurveyResponseDetails",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_AspNetUsers_AccountId",
                table: "Sponsors",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyResponseDetails_AspNetUsers_AccountId",
                table: "SurveyResponseDetails",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_AspNetUsers_AccountId",
                table: "Sponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyResponseDetails_AspNetUsers_AccountId",
                table: "SurveyResponseDetails");

            migrationBuilder.DropIndex(
                name: "IX_SurveyResponseDetails_AccountId",
                table: "SurveyResponseDetails");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "SurveyResponseDetails");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Sponsors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_AccountId",
                table: "SurveyResponses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_SurveyId",
                table: "SurveyResponses",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_AspNetUsers_AccountId",
                table: "Sponsors",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
