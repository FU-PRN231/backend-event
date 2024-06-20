﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("get-location-by-eventid/{id}")]
        public async Task<AppActionResult> GetLocationByEventId(Guid id)
        {
            return await _locationService.GetLocationByEventId(id);
        }

        [HttpGet("get-available-location/{startTime}/{endTime}")]
        public async Task<AppActionResult> GetLocationByEventId(DateTime startTime, DateTime endTime)
        {
            return await _locationService.GetAvailableLocation(startTime, endTime);
        }
    }
}