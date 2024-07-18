using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class UpdateAttendeeReponse
    {
        public bool CheckedIn { get; set; }
        public DateTime OrderDate { get; set; }
        public Account Account { get; set; }
        public SeatRank SeatRank { get; set; }
    }
}
