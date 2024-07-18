using AutoMapper;
using DocumentFormat.OpenXml.Office2013.Excel;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class SurveyService : GenericBackendService, ISurveyService
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
                if(dto.AccountId != null)
                {
                    var accountDb = await accountRepository.GetById(dto.AccountId);
                    if (accountDb == null)
                    {
                        result = BuildAppActionResultError(result, $"Không tìm thấy người dùng với id {dto.AccountId}");
                        return result;
                    }
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
                    result = BuildAppActionResultError(result, $"Không tìm thấy sự kiện với id {dto.EventId}");
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

        public async Task<AppActionResult> GellAllSurvey()
        {
            AppActionResult result = new();
            try
            {
                var surveyDb = await _surveyRepository.GetAllServey(null, 0, 0, null, false, s => s.CreateByAccount);
                List<SurveyQuestionResponse> data = new List<SurveyQuestionResponse>();
                if(surveyDb.Items != null && surveyDb.Items.Count > 0) 
                {
                    foreach( var item in surveyDb.Items)
                    {
                        var surveyQuestion = await _surveyQuestionDetailRepository.GetAllSurveyDetail(s => s.SurveyId == item.Id, 0, 0, null, false, null);
                        data.Add(new SurveyQuestionResponse
                        {
                            Survey = item,
                            surveyQuestionDetails = surveyQuestion.Items!
                        });
                    }
                    result.Result = data;
                }
            } catch(Exception ex) 
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GellSurveyOfOrganization(Guid id)
        {
            AppActionResult result = new();
            try
            {
                var organizationRepository = Resolve<IOrganizationRepository>();
                var organizationDb = await organizationRepository.GetById(id);
                if(organizationDb == null) 
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy tổ chức với id {id}");
                    return result;
                }

                var surveyDb = await _surveyRepository.GetAllServey(s => s.Event!.OrganizationId == id, 0, 0, null, false, s => s.CreateByAccount);
                List<SurveyQuestionResponse> data = new List<SurveyQuestionResponse>();
                if (surveyDb.Items != null && surveyDb.Items.Count > 0)
                {
                    foreach (var item in surveyDb.Items)
                    {
                        var surveyQuestion = await _surveyQuestionDetailRepository.GetAllSurveyDetail(s => s.SurveyId == item.Id, 0, 0, null, false, null);
                        data.Add(new SurveyQuestionResponse
                        {
                            Survey = item,
                            surveyQuestionDetails = surveyQuestion.Items!
                        });
                    }
                    result.Result = data;
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
                SurveyQuestionResponse data = new SurveyQuestionResponse();
                var surveyDb = await _surveyRepository.GetServeyById(surveyId, s => s.CreateByAccount);
                if(surveyDb == null ) 
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy form khảo sát với id {surveyId}");
                    return result;
                }
                data.Survey = surveyDb;
                var surveyQuestionDetail = await _surveyQuestionDetailRepository.GetAllSurveyDetail(s => s.SurveyId == surveyId, 0, 0, null, false, null);
                data.surveyQuestionDetails = surveyQuestionDetail.Items!;
                result.Result = data;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSurveyResponseBySurveyId(Guid id)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                SurveyAnswerResponse data = new SurveyAnswerResponse();
                var surveyDb = await _surveyRepository.GetServeyById(id, s => s.CreateByAccount);
                if (surveyDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy form khảo sát với id {id}");
                    return result;
                }
                data.Survey = surveyDb;
                var surveyQuestionDetail = await _surveyQuestionDetailRepository.GetAllSurveyDetail(s => s.SurveyId == id, 0, 0, null, false, null);
                var surveyAnswerDetailRepository = Resolve<ISurveyResponseDetailRepository>(); 
                foreach (var question in surveyQuestionDetail.Items!)
                {
                    SurveyAnswerDetailDto answers = new SurveyAnswerDetailDto();
                    answers.SurveyQuestionDetail = question;
                    var surveyAnswerDetail = await surveyAnswerDetailRepository.GetAllSurveyResponseDetail(s => s.SurveyQuestionId == question.Id, 0, 0, null, false, null);
                    answers.SurveyResponseDetails.AddRange(surveyAnswerDetail.Items!);
                    data.SurveyAnswerDetailDtos.Add(answers);
                }
                result.Result = data;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
