using Microsoft.AspNetCore.Identity;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class IdentityRoleRepository : Resolver, IIdentityRoleRepository
    {
        public readonly IGenericDAO<IdentityRole> _dao;
        public readonly IUnitOfWork _unitOfWork;

        public IdentityRoleRepository(IGenericDAO<IdentityRole> dao, IUnitOfWork unitOfWork,
        IServiceProvider serviceProvider
            ) : base(serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _dao = dao;
        }

        public async Task<Microsoft.AspNetCore.Identity.IdentityRole> GetIdentityRoleByName(string name)
        {
            return await _dao.GetByExpression(i => i.NormalizedName.Equals(name.ToLower()));
        }
    }
}