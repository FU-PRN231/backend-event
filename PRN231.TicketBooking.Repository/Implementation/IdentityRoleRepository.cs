using Microsoft.AspNetCore.Identity;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class IdentityRoleRepository : GenericRepository<IdentityRole>, IIdentityRoleRepository
    {

        public IdentityRoleRepository(IGenericDAO<IdentityRole> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
        }
        public async Task<Microsoft.AspNetCore.Identity.IdentityRole> GetIdentityRoleByName(string name)
        {
            return await this.GetByExpression(i => i.NormalizedName.Equals(name.ToLower()));
        }

		public async Task<List<string>> GetRoleNameListById(List<string> roleIds)
		{
            var roleList = await this.GetAllDataByExpression(i => roleIds.Contains(i.Id), 0, 0, null, false, null);
            if(roleList.Items.Count > 0)
            {
                return roleList.Items.DistinctBy(i => i.Id).Select(i => i.Name).ToList();
            }
            return new List<string>();
		}
	}
}