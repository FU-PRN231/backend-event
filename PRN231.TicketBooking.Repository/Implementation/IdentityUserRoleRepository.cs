using Microsoft.AspNetCore.Identity;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
	public class IdentityUserRoleRepository : GenericRepository<IdentityUserRole<string>>, IIdentityUserRoleRepository
    {
		private IUnitOfWork _unitOfWork;
        public IdentityUserRoleRepository(IUnitOfWork unitOfWork, IGenericDAO<IdentityUserRole<string>> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
			_unitOfWork = unitOfWork;
        }

		public async Task<bool> AssignRole(string userId, string roleId)
		{
			var userRole = await this.Insert(new IdentityUserRole<string> { 
				UserId = userId,
				RoleId = roleId
			});
			await _unitOfWork.SaveChangeAsync();
			return userRole != null;
		}

		public async Task<List<string>> GetRoleListByAccountId(string userId)
		{
			var roleListDb = await this.GetAllDataByExpression(r => r.UserId.Equals(userId), 0, 0, null, false, null);
			if(roleListDb.Items == null || roleListDb.Items.Count == 0)
			{
				return new List<string>();
			}
			return roleListDb.Items.Select(r => r.RoleId).ToList();
		}
	}
}
