using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
	public class EventRevenueReportDto
	{
		public int NumOfEvents { get; set; }
		public int NumOfSeat {  get; set; }
		public int NumOfBookedSeat { get; set; }
		public double TotalRevenue { get; set; }
		public double TotalSponsor { get; set; }
		public double TotalCost { get; set; }
	}
}
