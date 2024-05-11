using FluentValidation;

namespace PRN231.TicketBooking.API.Installers
{
    public class ValidatorInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<HandleErrorValidator>();
            //services.AddValidatorsFromAssemblyContaining<BlogRequest>();
            
        }
    }
}