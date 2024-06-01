using AutoMapper;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class SurveyService: GenericBackendService, ISurveyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyQuestionDetailRepository _surveyQuestionDetailRepository;

        public SurveyService(IMapper mapper, IUnitOfWork unitOfWork, ISurveyRepository surveyRepository, ISurveyQuestionDetailRepository surveyQuestionDetailRepository, IServiceProvider serviceProvider):base(serviceProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _surveyRepository = surveyRepository;
            _surveyQuestionDetailRepository = surveyQuestionDetailRepository;
        }

        public async Task<AppActionResult> CreateAnswerForSurvey(CreateAnswerRequest dto)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var accountRepository = Resolve<IAccountRepository>();
                var surveyResponseDetailRepository = Resolve<ISurveyResponseDetailRepository>();
                var accountDb = await accountRepository.GetById(dto.AccountId);
                if (accountDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy người dùng với id {dto.AccountId}");
                    return result;
                }
                var surveyDb = await _surveyRepository.GetById(dto.SurveyId);
                if(surveyDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy form khảo sát với id {dto.AccountId}");
                    return result;
                }

                bool addAnswers = await surveyResponseDetailRepository.AddAnswerToSurvey(dto.AnswerDetails, dto.AccountId);
                if(addAnswers)
                {
                    await _unitOfWork.SaveChangeAsync();
                }

            } catch (Exception ex) {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> CreateSurveyForm(CreateSurveyFormRequest dto)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository!.GetById(dto.EventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy sự kiện với id {eventDb.Id}");
                    return result;
                }

                Guid surveyId = await _surveyRepository.AddSurvey(dto);
                if(surveyId == Guid.Empty)
                {
                    result = BuildAppActionResultError(result, $"Tạo sự kiện không thành công");
                    return result;
                }
                bool surveyQuestion = await _surveyQuestionDetailRepository.InsertRangeSurveyQuestionDetail(surveyId, dto.QuestionDetailRequests);
                if (surveyQuestion)
                {
                    await _unitOfWork.SaveChangeAsync();
                } else
                {
                    result = BuildAppActionResultError(result, $"Tạo sự kiện không thành công");
                    return result;
                }

            } catch(Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSurveyById(Guid surveyId)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var surveyDb = await _surveyRepository.GetById(surveyId);
                if(surveyDb == null ) 
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy form khảo sát với id {surveyId}");
                    return result;
                }
                var surveyQuestionDetail = await _surveyQuestionDetailRepository.GetAllDataByExpression(s => s.SurveyId == surveyId, 0, 0, null, false, null);
                result.Result = surveyQuestionDetail;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
