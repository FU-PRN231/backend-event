using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class OrganzationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        private readonly IGenericDAO<Organization> _organizationDAO;
        public OrganzationRepository(IGenericDAO<Organization> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _organizationDAO = dao;
        }

        public async Task<PagedResult<Organization>> GetOrganizations(int pageNumber, int pageSize)
        {
            PagedResult<Organization> result = null;
            try
            {
                result = new PagedResult<Organization>();
                result = await _organizationDAO.GetAllDataByExpression(
                    filter: null,
                    pageNumber: pageNumber,
                    pageSize: pageSize
                );
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
