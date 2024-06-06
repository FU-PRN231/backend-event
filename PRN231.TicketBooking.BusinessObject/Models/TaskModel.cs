using PRN231.TicketBooking.BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class TaskModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;   
        public string Description { get; set; } = null!;    
        public string PersonInChargeName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public double Cost { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public Guid EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public Event? Event { get; set; }

    }
}
