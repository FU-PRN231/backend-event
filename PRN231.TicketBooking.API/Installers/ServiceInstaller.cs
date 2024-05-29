using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.DAO.Data;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;
using PRN231.TicketBooking.Service.Implementation;

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
            services.AddScoped<ISponsorRepository, SponsorRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISponsorService, SponsorService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            //Event
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventRepository, EventRepository>();
            //SeatRank
            services.AddScoped<ISeatRankService, SeatRankService>();
            services.AddScoped<ISeatRankRepository, SeatRankRepository>();
            //Attendee
            services.AddScoped<IAttendeeRepostory, AttendeeRepository>();
            services.AddScoped<IAttendeeService, AttendeeService>();
            //Order
            services.AddScoped<IOrderRepositoty, OrderRepository>();
        }
    }
}