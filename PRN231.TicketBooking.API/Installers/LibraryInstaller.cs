using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
//using Firebase.Storage;
using OfficeOpenXml;
using PRN231.TicketBooking.Common.Mapping;
using PRN231.TicketBooking.Common.Util;

namespace PRN231.TicketBooking.API.Installers
{
    public class LibraryInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<BackEndLogger>();
            //services.AddSingleton(_ => new FirebaseStorage(configuration["Firebase:Bucket"]));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            services.AddSingleton<Utility>();
            services.AddSingleton<SD>();
            services.AddSingleton<TemplateMappingHelper>();
        }
    }
}