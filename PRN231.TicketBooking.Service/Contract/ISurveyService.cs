﻿using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface ISurveyService
    {
        public Task<AppActionResult> CreateSurveyForm(CreateSurveyFormRequest dto);
        public Task<AppActionResult> GetSurveyById(Guid surveyId);
        public Task<AppActionResult> CreateAnswerForSurvey(CreateAnswerRequest dto);
    }
}