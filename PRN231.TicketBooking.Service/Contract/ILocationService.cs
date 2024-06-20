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
        public Task<AppActionResult> GetLocationByEventId(Guid eventId);
        public Task<AppActionResult> GetAllLocation();
    }
}
