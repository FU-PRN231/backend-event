using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<PagedResult<Post>> GetAllPost(Expression<Func<Post, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Post, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Post, object>>[]? includes);
    }
}
