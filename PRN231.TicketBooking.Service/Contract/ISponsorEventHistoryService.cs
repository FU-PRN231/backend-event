using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface ISponsorEventHistoryService
    {
        Task<AppActionResult> GetSponsorHistoryOfEvent(Guid eventId, int pageNumber, int pageSize);
        Task<AppActionResult> GetSponsorHistoryById(Guid id);
        Task<AppActionResult> GetSponsorHistoryByType(SponsorType sponsorType, int pageNumber, int pageSize);
        Task<AppActionResult> GetSponsorHistoryBySponsorId(Guid sponsorId, int pageNumber, int pageSize);
    }
}
