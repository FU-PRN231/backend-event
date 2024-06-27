using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.DAO.Data;

namespace PRN231.TicketBooking.API.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingTicketDbContext>(option =>
            {
                option.UseSqlServer(configuration["ConnectionStrings:DB"]);
            });

            services.AddIdentity<Account, IdentityRole>().AddEntityFrameworkStores<BookingTicketDbContext>().AddDefaultTokenProviders();
        }
    }
}