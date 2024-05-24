using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ISponsorRepository : IRepository<Sponsor>
    {
        public Task<Sponsor> GetSponsorByName(string name);

        public Task<List<Sponsor>> CreateSponsor(Dictionary<string, SponsorDto> dto);
    }
}