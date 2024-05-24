﻿using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.DAO.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IAccountRepository:IRepository<Account>
    {
        public Task<List<Account>> CreateSponsorAccount(CreateSponsorDto dto);
        public Task<Account> GetAccountByEmail(string email, bool? IsDeleted = false, bool? IsVerified = true);
    }
}
