using PRN231.TicketBooking.BusinessObject.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class SurveyQuestionDetail
    {
        [Key]
        public Guid Id { get; set; }

        public string Question { get; set; } = null!;
        public AnswerType? AnswerType { get; set; }
        public int? RatingMax { get; set; }
        public Guid SurveyId { get; set; }

        [ForeignKey(nameof(SurveyId))]
        public Survey? Survey { get; set; }
    }
}