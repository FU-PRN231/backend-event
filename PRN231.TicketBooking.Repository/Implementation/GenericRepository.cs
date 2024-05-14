using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.DAO.dao;

namespace PRN231.TicketBooking.Repository.Implementation;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly GenericDAO<T> genericDAO;

    public GenericRepository()
    {
        genericDAO = new GenericDAO<T> ();
    }
    public async Task<PagedResult<T>> GetAllDataByExpression(Expression<Func<T, bool>>? filter,
       int pageNumber, int pageSize,
       Expression<Func<T, object>>? orderBy = null,
       bool isAscending = true,
       params Expression<Func<T, object>>[]? includes)
    {
      return await genericDAO.GetAllDataByExpression(filter, pageNumber, pageSize, orderBy, isAscending, includes);
    }

    public async Task<T> GetById(object id)
    {
        return await genericDAO.GetById(id);
    }

    public async Task<T> Insert(T entity)
    {
        return await genericDAO.Insert(entity);
    }

    public async Task<T> Update(T entity)
    {
        return await genericDAO.Update(entity);
    }

    public async Task<T> DeleteById(object id)
    {
        return await genericDAO.DeleteById(id);
    }

    public async Task<T> GetByExpression(Expression<Func<T, bool>> filter,
        params Expression<Func<T, object>>[] includeProperties)
    {
        return await genericDAO.GetByExpression(filter, includeProperties);
    }

    public async Task<List<T>> InsertRange(IEnumerable<T> entities)
    {
       return await genericDAO.InsertRange(entities);
    }

    public async Task<List<T>> DeleteRange(IEnumerable<T> entities)
    {
        return await genericDAO.DeleteRange(entities);
    }
}