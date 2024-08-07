﻿using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class LocationService : GenericBackendService, ILocationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public LocationService(IServiceProvider serviceProvider, IMapper mapper, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppActionResult> GetAvailableLocation(DateTime StartTime, DateTime EndTime)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var locationRepository = Resolve<ILocationRepository>();
                var data = await locationRepository.GetAllLocation(StartTime, EndTime);
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultSuccess(result, "Thành công");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetLocationByEventId(Guid eventId)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var locationRepository = Resolve<ILocationRepository>();
                var data = await locationRepository.GetLocationByEventId(eventId);
                if (data == null)
                {
                    return BuildAppActionResultSuccess(result, $"Không tìm thấy location với event id {eventId}");
                }
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultSuccess(result, "Get location by  event id successfully");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
