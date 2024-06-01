﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class CreateSeatRankResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RemainingCapacity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public Guid EventId { get; set; }
    }
}