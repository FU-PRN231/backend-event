using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = PRN231.TicketBooking.BusinessObject.Enum.TaskStatus;

namespace PRN231.TicketBooking.Common.Dto.Response
{
	public class EventReportDto
	{
		public Event Event { get; set; }
        public int NumOfSeat { get; set; }
        public int NumOfBookedSeat { get; set; }
        public double TotalRevenue { get; set; }
        public double TotalTicketRevenue { get; set; }
        public double TotalSponsor { get; set; }
        public double TotalSponsorAmount { get; set; }
        public double TotalCost { get; set; }
        public Dictionary<string, int> TaskCompletion {  get; set; } = new Dictionary<string, int>();
		public List<SponsorMoneyHistory> SponsorMoneyHistories { get; set; } = new List<SponsorMoneyHistory>();
		public List<BusinessObject.Models.Task> Tasks { get; set; } = new List<BusinessObject.Models.Task>();
		public Dictionary<string, Dictionary<string, int>> SeatRegistration { get; set; } = new Dictionary<string, Dictionary<string, int>>();
	}

}
