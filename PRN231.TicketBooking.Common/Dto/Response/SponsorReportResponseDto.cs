using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class SponsorReportResponseDto
    {
        public Sponsor Sponsor { get; set; }
        public Dictionary<string, int> SponsorTypes { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, double> SponsorAmounts { get; set; } = new Dictionary<string, double>();
        public Dictionary<string, Dictionary<string, double>> EventDetailSponsors { get; set; } = new Dictionary<string, Dictionary<string, double>>();
    }
}
