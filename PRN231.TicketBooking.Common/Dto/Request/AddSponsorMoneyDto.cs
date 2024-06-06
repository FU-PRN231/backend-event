using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class AddSponsorMoneyDto
    {
        public Guid EventId { get; set; }
        public List<SponsorItemDto> SponsorItems { get; set; } = new List<SponsorItemDto>();
    }

    public class SponsorItemDto
    {
        public SponsorType SponsorType { get; set; }
        public string SponsorDescription { get; set; } = null!;
        public double? MoneySponsorAmount { get; set; }
        public Guid SponsorId { get; set; }
    }
}
