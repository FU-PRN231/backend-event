using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class GetEventByIdResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public List<StaticFile> StaticFiles { get; set; }
    }
}
