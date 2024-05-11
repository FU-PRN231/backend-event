using PRN231.TicketBooking.BusinessObject.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Survey:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public Event? Event { get; set; }
    }
}
