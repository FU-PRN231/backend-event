using PRN231.TicketBooking.BusinessObject.Models.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Organization
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime FoundedDate { get; set; }
        public string ContactEmail { get; set; } = null!;
        public string Website { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Img { get; set; } = null!;
        public DateTime CreateDate { get; set; }
    }
}