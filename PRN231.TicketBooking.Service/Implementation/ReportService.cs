using DocumentFormat.OpenXml.Drawing.Charts;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
	public class ReportService : GenericBackendService, IReportService
	{
		private readonly ISponsorMoneyHistoryRepository _sponsorMoneyHistoryRepository;
		private readonly ITaskRepository _taskRepository;
		private readonly IAccountRepository _accountRepository;
		public ReportService(ISponsorMoneyHistoryRepository sponsorMoneyHistoryRepository, ITaskRepository taskRepository, IAccountRepository accountRepository, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_sponsorMoneyHistoryRepository = sponsorMoneyHistoryRepository;
			_taskRepository = taskRepository;
			_accountRepository = accountRepository;
		}

		public async Task<AppActionResult> GetEventDetailReport(Guid eventId)
		{
            AppActionResult result = new AppActionResult();
            try
            {
                EventReportDto data = new EventReportDto();
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetEventById(eventId);
                data.Event = eventDb;
                await GetRevenueReport(eventDb, data);
                result.Result = data;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

		public async Task<AppActionResult> GetRevenueReport(Guid? organizationId, int timePeriod)
		{
            AppActionResult result = new AppActionResult();
            try
            {
                EventRevenueReportDto data = new EventRevenueReportDto();
                var utility = Resolve<Utility>();
                var eventRepository = Resolve<IEventRepository>();
                Common.Dto.Response.PagedResult<Event> eventDb = null;
                if(organizationId != null)
                {
                    eventDb = await eventRepository.GetAllDataByExpression(e => e.OrganizationId == organizationId, 0, 0, null, false, null);
                } else
                {
                    eventDb = await eventRepository.GetAllDataByExpression(null, 0, 0, null, false, null);
                }

                if(eventDb.Items.Count == 0)
                {
                    result.Result = data;
                    return result;
                }

                if (timePeriod > 3)
                {
                    List<Event> events = eventDb.Items.Where(e => e.StartTime.Year == timePeriod).ToList();
                    data = await GetRevenueReport(events);

                }
                else if (timePeriod == 0)
                {
                    List<Event> events = eventDb.Items.Where(e => e.StartTime.AddDays(7) >= utility.GetCurrentDateInTimeZone()).ToList();
                    data = await GetRevenueReport(events);
                }
                else if (timePeriod == 1)
                {
                    List<Event> events = eventDb.Items.Where(e => e.StartTime.Month >= DateTime.UtcNow.Month).ToList();
                    data = await GetRevenueReport(events);
                }
                else if (timePeriod == 0)
                {
                    List<Event> events = eventDb.Items.Where(e => e.StartTime.Month + 6 >= DateTime.UtcNow.Month).ToList();
                    data = await GetRevenueReport(events);
                }
                else
                {
                    List<Event> events = eventDb.Items.Where(e => e.StartEventDate.Year >= DateTime.UtcNow.Year).ToList();
                    data = await GetRevenueReport(events);
                }
                result.Result = data;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        private async Task<EventRevenueReportDto> GetRevenueReport(List<Event> events)
        {
            EventRevenueReportDto data = new EventRevenueReportDto();
            try
            {
                data.NumOfEvents = events.Count;
                var eventIds = events.Select(e => e.Id).ToList();

                var seatRankRepository = Resolve<ISeatRankRepository>();
                var seatRankDb = await seatRankRepository.GetAllDataByExpression(o => eventIds.Contains(o.EventId), 0, 0, null, false, null);
                seatRankDb.Items.ForEach(s => data.NumOfSeat += s.Quantity);
                seatRankDb.Items.ForEach(s => data.NumOfBookedSeat += s.Quantity - s.RemainingCapacity);

                var orderDetailRepository = Resolve<IOrderDetailsRepository>();
                var orderDetailDb = await orderDetailRepository.GetAllDataByExpression(o => eventIds.Contains(o.SeatRank.EventId), 0, 0, null, false, o => o.SeatRank);
                orderDetailDb.Items.ForEach(o => data.TotalTicketRevenue += o.Quantity * o.SeatRank.Price);

                var taskRepository = Resolve<ITaskRepository>();
                var taskDb = await taskRepository.GetAllDataByExpression(o => eventIds.Contains(o.EventId), 0, 0, null, false, null);
                taskDb.Items.ForEach(o => data.TotalCost += o.Cost);

                var sponsorMoneyHistoryRepository = Resolve<ISponsorMoneyHistoryRepository>();
                var sponsorMoneyHistoryDb = await sponsorMoneyHistoryRepository.GetAllDataByExpression(o => eventIds.Contains(o.EventSponsor.EventId), 0, 0, null, false, s => s.EventSponsor.Event);
                sponsorMoneyHistoryDb.Items.DistinctBy(s => s.EventSponsorId).Select(s => s.EventSponsorId).ToList().ForEach(o => data.TotalSponsor++);
                sponsorMoneyHistoryDb.Items.DistinctBy(s => s.Id).ToList().ForEach(o => data.TotalSponsorAmount += (o.IsFromSponsor) ? o.Amount : 0);
                data.TotalRevenue += data.TotalTicketRevenue + data.TotalSponsorAmount - data.TotalCost;
                return data;
            } 
            catch(Exception ex)
            {
                data = new EventRevenueReportDto();
            }
            return data;
        }
        private async System.Threading.Tasks.Task GetRevenueReport(Event eventDb, EventReportDto result)
        {
            EventRevenueReportDto data = new EventRevenueReportDto();
            try
            {

                var seatRankRepository = Resolve<ISeatRankRepository>();
                var seatRankDb = await seatRankRepository.GetAllDataByExpression(o => eventDb.Id == o.EventId, 0, 0, null, false, null);
                seatRankDb.Items.ForEach(s => result.NumOfSeat += s.Quantity);
                seatRankDb.Items.ForEach(s => result.NumOfBookedSeat += s.Quantity - s.RemainingCapacity);

                var orderDetailRepository = Resolve<IOrderDetailsRepository>();
                var orderDetailDb = await orderDetailRepository.GetAllDataByExpression(o => eventDb.Id == o.SeatRank.EventId, 0, 0, null, false, o => o.SeatRank, o => o.Order);
                orderDetailDb.Items.ForEach(o => result.TotalTicketRevenue += o.Quantity * o.SeatRank.Price);

                var taskRepository = Resolve<ITaskRepository>();
                var taskDb = await taskRepository.GetAllDataByExpression(o => eventDb.Id == o.EventId , 0, 0, null, false, null);
                taskDb.Items.ForEach(o => result.TotalCost += o.Cost);

                var sponsorMoneyHistoryRepository = Resolve<ISponsorMoneyHistoryRepository>();
                var sponsorMoneyHistoryDb = await sponsorMoneyHistoryRepository.GetAllDataByExpression(o => eventDb.Id == o.EventSponsor.EventId, 0, 0, null, false, s => s.EventSponsor.Event);
                sponsorMoneyHistoryDb.Items.DistinctBy(s => s.EventSponsorId).Select(s => s.EventSponsorId).ToList().ForEach(o => result.TotalSponsor++);
                sponsorMoneyHistoryDb.Items.DistinctBy(s => s.Id).ToList().ForEach(o => result.TotalSponsorAmount += (o.IsFromSponsor) ? o.Amount : 0);
                result.TotalRevenue += result.TotalTicketRevenue + result.TotalSponsorAmount - result.TotalCost;
                
                result.SponsorMoneyHistories = sponsorMoneyHistoryDb.Items;
                result.Tasks = taskDb.Items;

               if(orderDetailDb.Items.Count > 0)
               {
                    var groupSeatRankBookedDb = orderDetailDb.Items.GroupBy(o => o.SeatRank.Name).ToDictionary(o => o.Key, o => o.GroupBy(a => a.Order.PurchaseDate.Date)
                                               .ToDictionary(group => group.Key.ToString("dd/MM/yyyy"), group => group.Count()));
                    result.SeatRegistration = groupSeatRankBookedDb;
               }

               if(taskDb.Items.Count > 0)
               {
                    var taskGrouped = taskDb.Items.GroupBy(t => t.Status.ToString()).ToDictionary(t => t.Key, t => t.Count());
                    result.TaskCompletion = taskGrouped;
               }
            }
            catch (Exception ex)
            {
                data = new EventRevenueReportDto();
            }

        }

        public async Task<AppActionResult> GetUserReport(Guid? organizationId, int timePeriod)
		{
            AppActionResult result = new AppActionResult();
            try
            {

            } catch(Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
		}

        public async Task<AppActionResult> GetSponsorReport(Guid sponsorId, int timePeriod)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                SponsorReportResponseDto data = new SponsorReportResponseDto();
                var sponsorRepository = Resolve<ISponsorRepository>();
                var sponsorDb = await sponsorRepository.GetById(sponsorId);
                if(sponsorDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy nhà tài trợ với id {sponsorId}");
                    //public Dictionary<string, int> SponsorTypes { get; set; } = new Dictionary<string, int>();
                    //public Dictionary<string, double> SponsorAmounts { get; set; } = new Dictionary<string, double>();
                    //public Dictionary<Event, Dictionary<DateTime, double>> eventDetailSponsors { get; set; } = new Dictionary<Event, Dictionary<DateTime, double>>();
                    return result;
                }
                data.Sponsor = sponsorDb;
                var sponsorEventRepository = Resolve<IEventSponsorRepository>();
                var sponsorEventDb = new PagedResult<EventSponsor>();
                var sponsorMoneyHistoryRepository = Resolve<ISponsorMoneyHistoryRepository>();
                var sponsorMoneyHistoryDb = new PagedResult<SponsorMoneyHistory>();
                var utility = Resolve<Utility>();
                DateTime today = utility.GetCurrentDateInTimeZone();
                if (timePeriod > 3)
                {
                    sponsorEventDb = await sponsorEventRepository.GetAllDataByExpression(s => s.SponsorId == sponsorId && s.Event.StartTime.Year == today.Year, 0, 0, null, false, null);
                    sponsorMoneyHistoryDb = await sponsorMoneyHistoryRepository.GetAllDataByExpression(s => s.EventSponsor.SponsorId == sponsorId && s.EventSponsor.Event.StartTime.Year >= today.Year, 0, 0, null, false, s => s.EventSponsor.Event);
                } else if(timePeriod == 0)
                {
                    sponsorEventDb = await sponsorEventRepository.GetAllDataByExpression(s => s.SponsorId == sponsorId && s.Event.StartTime.AddDays(7) >= today, 0, 0, null, false, null);
                    sponsorMoneyHistoryDb = await sponsorMoneyHistoryRepository.GetAllDataByExpression(s => s.EventSponsor.SponsorId == sponsorId && s.EventSponsor.Event.StartTime.AddDays(7) >= today, 0, 0, null, false, s => s.EventSponsor.Event);
                }
                else if (timePeriod == 1)
                {
                    sponsorEventDb = await sponsorEventRepository.GetAllDataByExpression(s => s.SponsorId == sponsorId && s.Event.StartTime.Month >= today.Month, 0, 0, null, false, null);
                    sponsorMoneyHistoryDb = await sponsorMoneyHistoryRepository.GetAllDataByExpression(s => s.EventSponsor.SponsorId == sponsorId && s.EventSponsor.Event.StartTime.Month >= today.Month, 0, 0, null, false, s => s.EventSponsor.Event);
                }
                else if (timePeriod == 2)
                {
                    sponsorEventDb = await sponsorEventRepository.GetAllDataByExpression(s => s.SponsorId == sponsorId && s.Event.StartTime.Month + 6 >= today.Month, 0, 0, null, false, null);
                    sponsorMoneyHistoryDb = await sponsorMoneyHistoryRepository.GetAllDataByExpression(s => s.EventSponsor.SponsorId == sponsorId && s.EventSponsor.Event.StartTime.Month + 6 >= today.Month, 0, 0, null, false, s => s.EventSponsor.Event);
                }
                else
                {
                    sponsorEventDb = await sponsorEventRepository.GetAllDataByExpression(s => s.SponsorId == sponsorId && s.Event.StartTime.Year  >= today.Year, 0, 0, null, false, null);
                    sponsorMoneyHistoryDb = await sponsorMoneyHistoryRepository.GetAllDataByExpression(s => s.EventSponsor.SponsorId == sponsorId && s.EventSponsor.Event.StartTime.Year >= today.Year, 0, 0, null, false, s => s.EventSponsor.Event);
                }

                if(sponsorEventDb.Items.Count > 0) {
                    var groupedSponsorEvent = sponsorEventDb.Items.GroupBy(s => s.SponsorType.ToString()).ToDictionary(s => s.Key, s => s.Count());
                    data.SponsorTypes = groupedSponsorEvent;
                }

                if (sponsorMoneyHistoryDb.Items.Count > 0)
                {
                    var groupedSponsorEvent = sponsorMoneyHistoryDb.Items.GroupBy(s => s.EventSponsor.SponsorType.ToString()).ToDictionary(s => s.Key, s => s.Sum(a => a.Amount));
                    data.SponsorAmounts = groupedSponsorEvent;
                    var groupedSponsorEventDetail = sponsorMoneyHistoryDb.Items.GroupBy(s => s.EventSponsor.Event.Title)
                                                                               .ToDictionary(s => s.Key, s => s.GroupBy(s => s.Date.Date.ToString("dd-MM-yyyy")).
                                                                                                                ToDictionary(s => s.Key, s => s.Sum(a => a.Amount)));
                    data.EventDetailSponsors = groupedSponsorEventDetail;
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
