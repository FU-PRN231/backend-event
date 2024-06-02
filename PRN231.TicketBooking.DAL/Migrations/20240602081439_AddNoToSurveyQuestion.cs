using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231.TicketBooking.DAO.Migrations
{
    public partial class AddNoToSurveyQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "SurveyQuestionDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "No",
                table: "SurveyQuestionDetails");
        }
    }
}
