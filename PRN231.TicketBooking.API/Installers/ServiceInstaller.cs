

using PRN231.TicketBooking.Service.Contract;
using PRN231.TicketBooking.Service.Implementation;

namespace PRN231.TicketBooking.API.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEventService, EventService>();
            
        }
    }
}