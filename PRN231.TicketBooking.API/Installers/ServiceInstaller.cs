using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.DAO.Data;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;
using PRN231.TicketBooking.Service.Implementation;
using PRN231.TicketBooking.Service.Payment.PaymentService;

namespace PRN231.TicketBooking.API.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddScoped(typeof(IGenericDAO<>), typeof(GenericDAO<>));
            services.AddScoped<IDbContext, BookingTicketDbContext>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IIdentityRoleRepository, IdentityRoleRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddScoped<ISponsorRepository, SponsorRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISurveyQuestionDetailRepository, SurveyQuestionDetailRepository>();
            services.AddScoped<ISurveyResponseDetailRepository, SurveyResponseDetailRepository>();
            services.AddScoped<IIdentityUserRoleRepository, IdentityUserRoleRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISponsorService, SponsorService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            //Event
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<IEventRepository, EventRepository>();
            //SeatRank
            services.AddScoped<ISeatRankService, SeatRankService>();
            services.AddScoped<ISeatRankRepository, SeatRankRepository>();
            //Attendee
            services.AddScoped<IAttendeeRepostory, AttendeeRepository>();
            services.AddScoped<IAttendeeService, AttendeeService>();
            //Order
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentGatewayService, PaymentGatewayService>();
            services.AddScoped<IStaticFileRepository, StaticFileRepository>();
            services.AddScoped<ISpeakerRepository, SpeakerRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IEventSponsorRepository, EventSponsorRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IOrganizationRepository, OrganzationRepository>();
            services.AddScoped<ISponsorRepository, SponsorRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ISponsorMoneyHistoryRepository, SponsorMoneyHistoryRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ISponsorEventHistoryService, SponsorHistoryService>();
        }
    }
}