using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
	public class OverallEventReportResponse
	{
		public int NumOfEvents { get; set; }
		public double totalRevenue { get; set; }
		public List<EventReportDetailResponse> eventReportDetailResponses { get; set; } = new List<EventReportDetailResponse>();
	}

	public class EventReportDetailResponse
	{
		public string Time { get; set; }
		public int NumOfSoldSeat { get; set; }
		public double Revenue { get; set; }
	}
}
