using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
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
        public AccountRepository()
        {
        }

        //public async Task<bool> AssignSponsorRole(List<Account> sponsors)
        //{
        //    bool isSucess = true;
        //    try
        //    {
        //        var identityRoleRepository = Resolve<IRepository<IdentityRole>>();
        //        var roleDb = await identityRoleRepository!.GetByExpression(r => r.Name.Equals("TOUR GUIDE"));
        //        foreach (var account in tourGuideAccountList)
        //        {
        //            var accountDb = await _accountRepository.GetByExpression(a => a.PhoneNumber == account.PhoneNumber);
        //            if (accountDb != null)
        //            {
        //                await AssignRoleForUserId(accountDb.Id, new List<string> { roleDb.Id });
        //            }
        //            else
        //            {
        //                isSucess = false;
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return isSucess;
        //}

        public async Task<List<Account>> CreateSponsorAccount(CreateSponsorDto dto)
        {
            List<Account> accounts = new List<Account>();   
            try
            {
                Account curr = null;
                foreach(var sponsor in dto.SponsorDtos)
                {
                    curr = await this.GetAccountByEmail(sponsor.Email, false, null);
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
                await this.InsertRange(accounts);
            } catch (Exception ex)
            {
               
            }
            return accounts;
        }

        public async Task<Account> GetAccountByEmail(string email, bool? IsDeleted = false, bool? IsVerified = true)
        {
            return await this.GetByExpression(u =>
                    u!.Email.ToLower() == email.ToLower() 
                    && (IsDeleted == null || u.IsDeleted == IsDeleted) 
                    && (IsVerified == null || u.IsVerified == IsVerified));
        }

      

        
    }
}
