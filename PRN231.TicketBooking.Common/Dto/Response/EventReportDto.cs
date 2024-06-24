using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
	public class EventReportDto
	{
		public Event Event { get; set; }
		public double TotalRevenue { get; set; }
		public double TotalSponsor { get; set; }
		public double TotalCost { get; set; }
		public List<SponsorMoneyHistory> SponsorMoneyHistories { get; set; } = new List<SponsorMoneyHistory>();
		public List<BusinessObject.Models.Task> Tasks { get; set; } = new List<BusinessObject.Models.Task>();
		
	}

	public class SeatRankReportDto
	{
		public SeatRank SeatRank { get; set; }
	}
}
