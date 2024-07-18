using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly IGenericDAO<Post> _dao;
        public PostRepository(IGenericDAO<Post> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao; 
        }

        public async Task<PagedResult<Post>> GetAllPost(Expression<Func<Post, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Post, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Post, object>>[]? includes)
        {
            PagedResult<Post> result = null;
            try
            {
                result = new PagedResult<Post>();
                result = await _dao.GetAllDataByExpression(filter, pageNumber, pageSize, orderBy, isAscending, includes);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
