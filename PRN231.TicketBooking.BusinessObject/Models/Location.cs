using System.ComponentModel.DataAnnotations;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; } = null!;
    }
}