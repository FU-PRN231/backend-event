using Microsoft.AspNet.Identity.EntityFramework;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.DAO.Data;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountRepository(IUnitOfWork unitOfWork ,IGenericDAO<Account> dao, IServiceProvider serviceProvider):base(dao, serviceProvider)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AssignSponsorRole(List<Account> sponsors)
        {
            bool isSucess = true;
            try
            {
                var identityRoleRepository =Resolve<IGenericDAO<IdentityRole>>();
                var roleDb = await identityRoleRepository!.GetByExpression(r => r.Name.Equals("TOUR GUIDE"));
              
            }
            catch (Exception ex)
            {
            }
            return isSucess;
        }

        public async Task<List<Account>> CreateSponsorAccount(CreateSponsorDto dto)
        {
            List<Account> accounts = new List<Account>();   
            try
            {
                Account curr = null;
                foreach(var sponsor in dto.SponsorDtos!)
                {
                    curr = await GetAccountByEmail(sponsor.Email, false, null);
                    if (curr != null)
                    {
                        accounts.Add(curr);
                    } else
                    {
                        var user = new Account
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = sponsor.Email,
                            UserName = sponsor.Email,
                            FirstName = sponsor.Name,
                            PhoneNumber = sponsor.PhoneNumber,
                            VerifyCode = null,

                        };
                        accounts.Add(user);
                    }
                }
                await _dao.InsertRange(accounts);
            } catch (Exception ex)
            {
               
            }
            return accounts;
        }

        public async Task<Account> GetAccountByEmail(string email, bool? IsDeleted = false, bool? IsVerified = true)
        {
            return await _dao.GetByExpression(u =>
                    u!.Email.ToLower() == email.ToLower() 
                    && (IsDeleted == null || u.IsDeleted == IsDeleted) 
                    && (IsVerified == null || u.IsVerified == IsVerified));
        }    
    }
}
