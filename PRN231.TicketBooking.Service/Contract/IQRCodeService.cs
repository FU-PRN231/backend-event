using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IQRCodeService
    {
        public Task<AppActionResult> GenerateQR(string Id);
        public Task<AppActionResult> DecodeQR(string hashedAccountData);
    }
}
