﻿using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ISurveyQuestionDetailRepository:IRepository<SurveyQuestionDetail>
    {
        public Task<bool> InsertRangeSurveyQuestionDetail(Guid eventId, List<QuestionDetailRequest> details);
    }
}
