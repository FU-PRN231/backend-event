using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class SurveyResponseDetail
    {
        [Key]
        public Guid Id { get; set; }

        public string? TextAnswer { get; set; }
        public int? Rating { get; set; }
        public Guid SurveyQuestionId { get; set; }

        [ForeignKey(nameof(SurveyQuestionId))]
        public SurveyQuestionDetail? Question { get; set; }

        public string? AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }
    }
}