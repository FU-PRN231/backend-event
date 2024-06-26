using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class AddSponsorMoneyHistoryRequestDto
    {
        public Guid SponsorEventId { get; set; }
        public double Amount { get; set; }
        public bool IsFromSponsor { get; set; }
        public DateTime? TransferredDate { get; set; }
    }
}
