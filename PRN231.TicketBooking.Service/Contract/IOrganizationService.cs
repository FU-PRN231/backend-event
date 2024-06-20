using PRN231.TicketBooking.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IOrganizationService
    {
        public Task<AppActionResult> GetAllOrganization(int pageNumber, int pageSize);
    }
}
