using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class SponsorMoneyHistory
    { 
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool IsFromSponsor { get; set; }
        public Guid EventSponsorId { get; set; }
        [ForeignKey(nameof(EventSponsorId))]
        public EventSponsor? EventSponsor { get; set; }
    }
}
