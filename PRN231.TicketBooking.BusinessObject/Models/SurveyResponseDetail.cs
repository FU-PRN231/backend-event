using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class SurveyResponseDetail
    {
        [Key]
        public Guid Id { get; set; }
        public string? TextAnswer { get; set; }
        public int? Rating {  get; set; }
        public Guid SurveyQuestionId { get; set; }
        [ForeignKey(nameof(SurveyQuestionId))]
        public SurveyQuestionDetail? Question { get; set; }
    }
}
