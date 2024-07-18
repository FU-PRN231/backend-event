using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using System.Linq.Expressions;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ISponsorRepository : IRepository<Sponsor>
    {
        public Task<Sponsor> GetSponsorByName(string name);

        public Task<List<Sponsor>> CreateListSponsor(Dictionary<string, CreateSponsorDto> dto);
        public Task<Sponsor> CreateSponsor(CreateSponsorDto dto);
        public Task<PagedResult<Sponsor>> GetAllSponsor(Expression<Func<Sponsor, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Sponsor, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Sponsor, object>>[]? includes);
    }
}