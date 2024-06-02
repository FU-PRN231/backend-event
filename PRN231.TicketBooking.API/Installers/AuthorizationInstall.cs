
using PRN231.TicketBooking.Common.Util;

namespace PRN231.TicketBooking.API.Installers
{
    public class AuthorizationInstall : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthorization(options => {

                options.AddPolicy("ADMIN", policy =>
                {
                    policy.RequireRole(SD.RoleConvert.ADMIN);
                });

                options.AddPolicy("MANAGEMENT", policy =>
                {
                    policy.RequireRole(SD.RoleConvert.ADMIN);
                    policy.RequireRole(SD.RoleConvert.STAFF);
                });

                options.AddPolicy("ORGANIZATION", policy =>
                {
                    policy.RequireRole(SD.RoleConvert.ADMIN);
                    policy.RequireRole(SD.RoleConvert.STAFF);
                    policy.RequireRole(SD.RoleConvert.ORGANIZER);
                    policy.RequireRole(SD.RoleConvert.PM);
                    policy.RequireRole(SD.RoleConvert.SPONSOR);
                });
            });
        }
    }
}
