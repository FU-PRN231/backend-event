﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231.TicketBooking.BusinessObject.Models
{
    public class Attendee
    {
        [Key]
        public Guid Id { get; set; }

        public bool CheckedIn { get; set; }
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event? Event { get; set; }
    }
}