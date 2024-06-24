using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
	public class ReportService : GenericBackendService, IReportService
	{
		private readonly ISponsorMoneyHistoryRepository _sponsorMoneyHistoryRepository;
		private readonly ITaskRepository _taskRepository;
		private readonly IAccountRepository _accountRepository;
		public ReportService(ISponsorMoneyHistoryRepository sponsorMoneyHistoryRepository, ITaskRepository taskRepository, IAccountRepository accountRepository, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_sponsorMoneyHistoryRepository = sponsorMoneyHistoryRepository;
			_taskRepository = taskRepository;
			_accountRepository = accountRepository;
		}

		public Task<AppActionResult> GetEventDetailReport(Guid eventId)
		{
			throw new NotImplementedException();
		}

		public Task<AppActionResult> GetRevenueReport(Guid? organizationId, int timePeriod)
		{
			throw new NotImplementedException();
		}

		public Task<AppActionResult> GetUserReport(int timePeriod)
		{
			throw new NotImplementedException();
		}
	}
}
