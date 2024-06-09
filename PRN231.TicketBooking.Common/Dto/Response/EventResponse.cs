using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class EventResponse
    {
        public Event Event { get; set; } = null!;
        public List<SeatRank> SeatRanks { get; set; } = null!;
        public List<Speaker> Speakers { get; set; } = null!;
        public List<EventSponsor> EventSponsors { get; set;}  = null!;    
        public List<Survey> Surveys { get; set; } = null!; 
        public List<Post> Posts { get; set; } = null!;      
        public List<StaticFile> StaticFiles { get; set; } = null!;      
    }
}
