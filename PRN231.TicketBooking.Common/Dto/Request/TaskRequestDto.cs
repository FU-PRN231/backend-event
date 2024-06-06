using PRN231.TicketBooking.BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = PRN231.TicketBooking.BusinessObject.Enum.TaskStatus;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class TaskRequestDto
    {
        public Guid EventId { get; set; }
        public List<TaskDetails> TaskDetails { get; set; } = null!;
    }

    public class TaskDetails
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PersonInChargeName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public double Cost { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class UpdateTask
    {
        public Guid Id { get; set; }    
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PersonInChargeName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public double Cost { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}
