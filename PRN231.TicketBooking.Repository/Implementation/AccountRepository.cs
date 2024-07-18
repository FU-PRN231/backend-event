using Microsoft.AspNet.Identity.EntityFramework;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System.Linq.Expressions;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericDAO<Account> _dao;

        public AccountRepository(IUnitOfWork unitOfWork, IGenericDAO<Account> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao; 
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AssignSponsorRole(List<Account> sponsors)
        {
            bool isSucess = true;
            try
            {
                var identityRoleRepository = Resolve<IGenericDAO<IdentityRole>>();
                var roleDb = await identityRoleRepository!.GetByExpression(r => r.Name.Equals("TOUR GUIDE"));
            }
            catch (Exception ex)
            {
            }
            return isSucess;
        }


        public async Task<Account> GetAccountByEmail(string email, bool? IsDeleted = false, bool? IsVerified = true)
        {
            return await _dao.GetByExpression(u =>
                    u!.Email.ToLower() == email.ToLower()
                    && (IsDeleted == null || u.IsDeleted == IsDeleted)
                    && (IsVerified == null || u.IsVerified == IsVerified));
        }

        public async Task<PagedResult<Account>> GetAllIAccount(Expression<Func<Account, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Account, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Account, object>>[]? includes)
        {
            PagedResult<Account> result = null;
            try
            {
                result = new PagedResult<Account>();
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