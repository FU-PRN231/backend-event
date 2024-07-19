using AutoMapper;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

        public async Task<AppActionResult> CreateOrganization(CreateOrganizationDto organizationDto)
        {
            var result = new AppActionResult();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var firebaseService = Resolve<IFirebaseService>();
                try
                {
                    var isOrganizationExisted = await _organizationRepository.GetByExpression(p => p.Name == organizationDto.Name);
                    if (isOrganizationExisted == null)
                    {
                        result = BuildAppActionResultError(result, $"Tổ chức với tên {organizationDto.Name} đã tồn tại");
                    }
                    var newOrgranization = new Organization
                    {
                        Id = Guid.NewGuid(),
                        Name = organizationDto.Name,
                        Address = organizationDto.Address,
                        ContactEmail = organizationDto.ContactEmail,
                        CreateDate = organizationDto.CreateDate,
                        Description = organizationDto.Description,
                        FoundedDate = organizationDto.FoundedDate,
                        Website = organizationDto.Website,
                    };

                    var pathName = SD.FirebasePathName.ORGANIZATION_PREFIX + $"{newOrgranization.Id}{Guid.NewGuid()}.jpg";
                    var upload = await firebaseService!.UploadFileToFirebase(organizationDto.File, pathName);
                    newOrgranization.Img = upload!.Result!.ToString()!;

                    if (!upload.IsSuccess)
                    {
                        result = BuildAppActionResultError(result, "Upload failed");

                    }
                    if (!BuildAppActionResultIsError(result))
                    {
                        await _organizationRepository.Insert(newOrgranization);
                        await _unitOfWork.SaveChangeAsync();
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    result = BuildAppActionResultError(result, ex.Message);
                }
                return result;
            }
        }

        public async Task<AppActionResult> DeleteOrganization(Guid id)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var organizationDb = await _organizationRepository.GetById(id);
                if (organizationDb == null)
                {
                    result = BuildAppActionResultError(result, $"Tổ chức với tên {id} không tồn tại");
                }
                await _organizationRepository.DeleteById(id);
                result.Messages.Add("Xóa tổ chức thành công");
                result.IsSuccess = true;    
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;  
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

        public async Task<AppActionResult> UpdateOrganization(UpdateOrganizationDTO organizationDTO)
        {
            var result = new AppActionResult();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var firebaseService = Resolve<IFirebaseService>();
                    var isExistedOrganization = await _organizationRepository.GetById(organizationDTO.Id);
                    if (isExistedOrganization == null)
                    {
                        result = BuildAppActionResultError(result, $"Tổ chức với id {organizationDTO.Id} không tồn tại");
                    }

                    var pathNameToDelete = SD.FirebasePathName.ORGANIZATION_PREFIX + $"{organizationDTO.Id}{Guid.NewGuid()}.jpg";
                    var imageResult = await firebaseService!.DeleteFileFromFirebase(pathNameToDelete);
                    if (imageResult != null)
                    {
                        result.Messages.Add("Delete image on firebase cloud successful");
                    }

                    var pathName = SD.FirebasePathName.ORGANIZATION_PREFIX + $"{organizationDTO.Id}{Guid.NewGuid()}.jpg";
                    var upload = await firebaseService!.UploadFileToFirebase(organizationDTO.File, pathName);

                    isExistedOrganization.Name = organizationDTO.Name;
                    isExistedOrganization.Address = organizationDTO.Address;
                    isExistedOrganization.CreateDate = organizationDTO.CreateDate;
                    isExistedOrganization.Description = organizationDTO.Description;
                    isExistedOrganization.FoundedDate = organizationDTO.FoundedDate;
                    isExistedOrganization.ContactEmail = organizationDTO.ContactEmail;
                    isExistedOrganization.Img = upload.Result!.ToString()!;

                    if (!BuildAppActionResultIsError(result))
                    {
                        await _organizationRepository.Update(isExistedOrganization);
                        await _unitOfWork.SaveChangeAsync();
                        result.Messages.Add("Update tổ chức thành công");
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    return BuildAppActionResultError(result, $"Có lỗi xảy ra {ex.Message}");
                }
                return result; 
            }
        }
    }
}
