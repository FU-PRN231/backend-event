﻿namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string RecoveryCode { get; set; }
        public string NewPassword { get; set; }
    }
}