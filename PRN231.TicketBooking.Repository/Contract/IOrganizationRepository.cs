﻿using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        public Task<PagedResult<Organization>> GetOrganizations(int pageNumber, int pageSize);
    }
}
