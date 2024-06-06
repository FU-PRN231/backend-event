﻿using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class EventSponsorRepository : GenericRepository<EventSponsor>, IEventSponsorRepository
    {
        private readonly IGenericDAO<EventSponsor> _dao;
        public EventSponsorRepository(IGenericDAO<EventSponsor> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao;
        }

        public async Task<EventSponsor> AddEventSponsorFromEvent(EventSponsor eventSponsor)
        {
            var data = await _dao.Insert(eventSponsor);
            if (data == null)
            {
                return null;
            }
            return data;
        }
    }
}