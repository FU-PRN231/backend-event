using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface ISeatRankService
    {
        public Task<AppActionResult> GetAllSeatRank(int pageNumber, int pageSize);
        public Task<AppActionResult> GetSeatRankById(Guid id);
        public Task<AppActionResult> GetAllSeatRankByEvent(Guid eventId, int pageNumber, int pageSize);
        public Task<AppActionResult> GetSeatRankByFilter(FilterSeatRankDto filterSeatRankDto, int pageNumber, int pageSize);
        public Task<AppActionResult> UpdateSeatRank(UpdateSeatRankDto updateSeatRankDto);
        public Task<AppActionResult> DeleteSeatRank(Guid seatrankId);
       // public Task<AppActionResult> AddSeatRank(CreateSeatRankDtoRequest dto);
    }
}
