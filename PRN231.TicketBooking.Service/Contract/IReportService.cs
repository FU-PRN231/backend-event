using PRN231.TicketBooking.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
	public interface IReportService
	{
		public Task<AppActionResult> GetRevenueReport(Guid? organizationId, int timePeriod);
		public Task<AppActionResult> GetUserReport(int timePeriod);
		public Task<AppActionResult> GetEventDetailReport(Guid eventId);
	}
}
