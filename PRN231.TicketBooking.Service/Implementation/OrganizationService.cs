using AutoMapper;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class OrganizationService : GenericBackendService, IOrganizationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IServiceProvider serviceProvider, IMapper mapper, IUnitOfWork unitOfWork, IOrganizationRepository organizationRepository) : base(serviceProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _organizationRepository = organizationRepository;
        }

        public async Task<AppActionResult> GetAllOrganization(int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var organizationRepo = Resolve<IOrganizationRepository>();
                var data = await organizationRepo.GetOrganizations(pageNumber, pageSize);
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultSuccess(result, "Get list sponsor successfully!");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
