using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
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
    public class GenericRepository<T> : Resolver, IRepository<T> where T : class
    {
        protected readonly IGenericDAO<T> _dao;
        public GenericRepository(IGenericDAO<T> dao ,IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _dao = dao;
        }

        public async Task<T?> DeleteById(object id) => await _dao.DeleteById(id);

        public async Task<List<T>> DeleteRange(IEnumerable<T> entities) => await _dao.DeleteRange(entities);

        public async Task<PagedResult<T>> GetAllDataByExpression(Expression<Func<T, bool>>? filter, int pageNumber, int pageSize, Expression<Func<T, object>>? orderBy = null, bool isAscending = true, params Expression<Func<T, object>>[]? includes)
        => await _dao.GetAllDataByExpression(filter, pageNumber, pageSize, orderBy, isAscending, includes);

        public async Task<T?> GetByExpression(Expression<Func<T?, bool>> filter, params Expression<Func<T, object>>[]? includeProperties)
        => await _dao.GetByExpression(filter, includeProperties);

        public async Task<T> GetById(object id) => await _dao.GetById(id);

        public async Task<T> Insert(T entity) => await _dao.Insert(entity);

        public async Task<List<T>> InsertRange(IEnumerable<T> entities) => await _dao.InsertRange(entities);

        public async Task<T> Update(T entity) => await _dao.Update(entity);
    }
}
