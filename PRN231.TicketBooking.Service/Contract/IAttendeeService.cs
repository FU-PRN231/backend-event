using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IAttendeeService
    {
        public Task<AppActionResult> CheckInAttendee(CheckInEventRequest checkInEvent); 
        public Task<AppActionResult> CheckInAttendee(string qrString);
        public Task<AppActionResult> GetAllAttendeeByEventId(Guid eventId);
	}
}
