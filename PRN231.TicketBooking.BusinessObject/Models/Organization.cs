using PRN231.TicketBooking.BusinessObject.Models.BaseModel;
using System.ComponentModel.DataAnnotations;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Organization : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FoundedDate { get; set; }
        public string ContactEmail { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
    }
}