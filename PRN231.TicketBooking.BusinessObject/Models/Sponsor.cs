using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Sponsor
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Img { get; set; } = null!;
    }
}
