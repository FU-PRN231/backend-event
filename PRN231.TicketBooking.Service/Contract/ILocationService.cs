using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface ILocationService
    {
        Task<AppActionResult> GetLocationByEventId(Guid eventId);
        Task<AppActionResult> GetAvailableLocation(DateTime StartTimeEvent, DateTime EndTimeEvent);
    }
}
