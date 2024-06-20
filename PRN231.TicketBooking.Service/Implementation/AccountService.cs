using AutoMapper;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.ConfigurationModel;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using QRCoder;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using IronBarCode;
using Firebase.Auth;
using System.Data.Entity.Core.Metadata.Edm;


namespace PRN231.TicketBooking.Service.Implementation
{
    public class AccountService : GenericBackendService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<Account> _signInManager;
        private readonly TokenDto _tokenDto;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;
        private readonly IEmailService _emailService;
        private readonly IFirebaseService _firebaseService;

        public AccountService(
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IEmailService emailService,
            IFirebaseService firebaseService,
            IMapper mapper,
            IServiceProvider serviceProvider
        ) : base(serviceProvider)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _firebaseService = firebaseService;
            _tokenDto = new TokenDto();
            _mapper = mapper;
        }

        public async Task<AppActionResult> Login(LoginRequestDto loginRequest)
        {
            var result = new AppActionResult();
            try
            {
                var user = await _accountRepository.GetAccountByEmail(loginRequest.Email, false, null);
                if (user == null)
                    result = BuildAppActionResultError(result, $" {loginRequest.Email} này không tồn tại trong hệ thống");
                else if (user.IsVerified == false)
                    result = BuildAppActionResultError(result, "Tài khoản này chưa được xác thực !");

                var passwordSignIn =
                    await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);
                if (!passwordSignIn.Succeeded) result = BuildAppActionResultError(result, "Đăng nhâp thất bại");
                if (!BuildAppActionResultIsError(result)) result = await LoginDefault(loginRequest.Email, user);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> VerifyLoginGoogle(string email, string verifyCode)
        {
            var result = new AppActionResult();
            try
            {
                var user = await _accountRepository.GetAccountByEmail(email, false, null);
                if (user == null)
                    result = BuildAppActionResultError(result, $"Email này không tồn tại");
                else if (user.IsVerified == false)
                    result = BuildAppActionResultError(result, "Tài khoản này chưa xác thực !");
                else if (user.VerifyCode != verifyCode)
                    result = BuildAppActionResultError(result, "Mã xác thực sai!");

                if (!BuildAppActionResultIsError(result))
                {
                    result = await LoginDefault(email, user);
                    user!.VerifyCode = null;
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> CreateAccount(SignUpRequestDto signUpRequest, bool isGoogle)
        {
            var result = new AppActionResult();
            try
            {
                if (await _accountRepository.GetAccountByEmail(signUpRequest.Email, null, null) != null)
                    result = BuildAppActionResultError(result, "Email hoặc username không tồn tại!");

                if (!BuildAppActionResultIsError(result))
                {
                    var emailService = Resolve<IEmailService>();
                    var verifyCode = string.Empty;
                    if (!isGoogle) verifyCode = Guid.NewGuid().ToString("N").Substring(0, 6);

                    var user = new Account
                    {
                        Email = signUpRequest.Email,
                        UserName = signUpRequest.Email,
                        FirstName = signUpRequest.FirstName,
                        LastName = signUpRequest.LastName,
                        PhoneNumber = signUpRequest.PhoneNumber,
                        Gender = signUpRequest.Gender,
                        VerifyCode = verifyCode,
                        IsVerified = isGoogle ? true : false
                    };
                    var resultCreateUser = await _userManager.CreateAsync(user, signUpRequest.Password);
                    if (resultCreateUser.Succeeded)
                    {
                        result.Result = user;
                        if (!isGoogle)
                            emailService!.SendEmail(user.Email, SD.SubjectMail.VERIFY_ACCOUNT,
                                TemplateMappingHelper.GetTemplateOTPEmail(
                                    TemplateMappingHelper.ContentEmailType.VERIFICATION_CODE, verifyCode,
                                    user.FirstName));
                        GenerateQR(user.Id);
                    }
                    else
                    {
                        result = BuildAppActionResultError(result, $"Tạo tài khoản không thành công");
                    }

                    var resultCreateRole = await _userManager.AddToRoleAsync(user, "CUSTOMER");
                    if (!resultCreateRole.Succeeded) result = BuildAppActionResultError(result, $"Cấp quyền khách hàng không thành công");
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> UpdateAccount(UpdateAccountRequestDto accountRequest)
        {
            var result = new AppActionResult();
            try
            {
                var account =
                    await _accountRepository.GetAccountByEmail(accountRequest.Email, null, null);
                if (account == null)
                    result = BuildAppActionResultError(result, $"Tài khoản với email {accountRequest.Email} không tồn tại!");
                if (!BuildAppActionResultIsError(result))
                {
                    account!.FirstName = accountRequest.FirstName;
                    account.LastName = accountRequest.LastName;
                    account.PhoneNumber = accountRequest.PhoneNumber;
                    result.Result = await _accountRepository.Update(account);
                }

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> GetAccountByUserId(string id)
        {
         
            var result = new AppActionResult();
            try
            {
                var organizationRepository = Resolve<IOrganizationRepository>();
                var sponsorRepository = Resolve<ISponsorRepository>();
                var account = await _accountRepository.GetById(id);
                if (account.OrganizationId != null)
                {
                    var organizationDb = await organizationRepository.GetByExpression(p => p!.Id == account.OrganizationId);
                    if (organizationDb == null)
                    {
                        result = BuildAppActionResultError(result, $"Tổ chức với {account.OrganizationId} không tồn tại");
                    }
                    account.Organization = organizationDb;
                }
                else if (account.SponsorId != null)
                {
                    var sponsorDb = await sponsorRepository.GetByExpression(p => p.Id == account.SponsorId);
                    if (sponsorDb == null)
                    {
                        result = BuildAppActionResultError(result, $"Nhà tài trợ với {account.SponsorId} không tồn tại");
                    }
                    account.Sponsor = sponsorDb;    
                }
                if (account == null) result = BuildAppActionResultError(result, $"Tài khoản với id {id} không tồn tại !");
                if (!BuildAppActionResultIsError(result)) result.Result = account;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> GetAllAccount(int pageIndex, int pageSize)
        {
            var result = new AppActionResult();
            var list = await _accountRepository.GetAllDataByExpression(null, pageIndex, pageSize, null, false, null);

            var userRoleRepository = Resolve<IIdentityUserRoleRepository>();
            var roleRepository = Resolve<IIdentityRoleRepository>();
            var listRole = await roleRepository!.GetAllDataByExpression(null, 1, 100, null, false, null);
            var listMap = _mapper.Map<List<AccountResponse>>(list.Items);
            foreach (var item in listMap)
            {
                var userRole = new List<IdentityRole>();
                var role = await userRoleRepository!.GetAllDataByExpression(a => a.UserId == item.Id, 1, 100, null, false, null);
                foreach (var itemRole in role.Items!)
                {
                    var roleUser = listRole.Items!.ToList().FirstOrDefault(a => a.Id == itemRole.RoleId);
                    if (roleUser != null) userRole.Add(roleUser);
                }

                item.Role = userRole;
            }

            result.Result =
                new PagedResult<AccountResponse>
                { Items = listMap, TotalPages = list.TotalPages };
            return result;
        }

        public async Task<AppActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var result = new AppActionResult();

            try
            {
                if (await _accountRepository.GetAccountByEmail(changePasswordDto.Email, false, null) == null)
                    result = BuildAppActionResultError(result,
                        $"Tài khoản có email {changePasswordDto.Email} không tồn tại!");
                if (!BuildAppActionResultIsError(result))
                {
                    var user = await _accountRepository.GetByExpression(c =>
                        c!.Email == changePasswordDto.Email && c.IsDeleted == false);
                    var changePassword = await _userManager.ChangePasswordAsync(user!, changePasswordDto.OldPassword,
                        changePasswordDto.NewPassword);
                    if (!changePassword.Succeeded)
                        result = BuildAppActionResultError(result, "Thay đổi mật khẩu thất bại");
                }

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> GetNewToken(string refreshToken, string userId)
        {
            var result = new AppActionResult();

            try
            {
                var user = await _accountRepository.GetById(userId);
                if (user == null)
                    result = BuildAppActionResultError(result, "Tài khoản không tồn tại");
                else if (user.RefreshToken != refreshToken)
                    result = BuildAppActionResultError(result, "Mã làm mới không chính xác");

                if (!BuildAppActionResultIsError(result))
                {
                    var jwtService = Resolve<IJwtService>();
                    result.Result = await jwtService!.GetNewToken(refreshToken, userId);
                }

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var result = new AppActionResult();

            try
            {
                var user = await _accountRepository.GetAccountByEmail(dto.Email, false, true);
                if (user == null)
                    result = BuildAppActionResultError(result, "Tài khoản không tồn tại hoặc chưa được xác thực!");
                else if (user.VerifyCode != dto.RecoveryCode)
                    result = BuildAppActionResultError(result, "Mã xác thực sai!");

                if (!BuildAppActionResultIsError(result))
                {
                    await _userManager.RemovePasswordAsync(user!);
                    var addPassword = await _userManager.AddPasswordAsync(user!, dto.NewPassword);
                    if (addPassword.Succeeded)
                        user!.VerifyCode = null;
                    else
                        result = BuildAppActionResultError(result, "Thay đổi mật khẩu thất bại. Vui lòng thử lại");
                }

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> ActiveAccount(string email, string verifyCode)
        {
            var result = new AppActionResult();
            try
            {
                var user = await _accountRepository.GetAccountByEmail(email, false, false);
                if (user == null)
                    result = BuildAppActionResultError(result, "Tài khoản không tồn tại ");
                else if (user.VerifyCode != verifyCode)
                    result = BuildAppActionResultError(result, "Mã xác thực sai");

                if (!BuildAppActionResultIsError(result))
                {
                    user!.IsVerified = true;
                    user.VerifyCode = null;
                }

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> SendEmailForgotPassword(string email)
        {
            var result = new AppActionResult();

            try
            {
                var user = await _accountRepository.GetAccountByEmail(email, false, true);
                if (user == null) result = BuildAppActionResultError(result, "Tài khoản không tồn tại hoặc chưa được xác thực");

                if (!BuildAppActionResultIsError(result))
                {
                    var emailService = Resolve<IEmailService>();
                    var code = await GenerateVerifyCode(user!.Email, true);
                    emailService?.SendEmail(email, SD.SubjectMail.PASSCODE_FORGOT_PASSWORD,
                        TemplateMappingHelper.GetTemplateOTPEmail(TemplateMappingHelper.ContentEmailType.FORGOTPASSWORD,
                            code, user.FirstName!));
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> SendEmailForActiveCode(string email)
        {
            var result = new AppActionResult();

            try
            {
                var user = await _accountRepository.GetAccountByEmail(email, false, false);
                if (user == null) result = BuildAppActionResultError(result, "Tài khoản không tồn tại hoặc chưa xác thực");

                if (!BuildAppActionResultIsError(result))
                {
                    var emailService = Resolve<IEmailService>();
                    var code = await GenerateVerifyCode(user!.Email, false);
                    emailService!.SendEmail(email, SD.SubjectMail.VERIFY_ACCOUNT,
                        TemplateMappingHelper.GetTemplateOTPEmail(TemplateMappingHelper.ContentEmailType.VERIFICATION_CODE,
                            code, user.FirstName!));
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<string> GenerateVerifyCode(string email, bool isForForgettingPassword)
        {
            var code = string.Empty;

            var user = await _accountRepository.GetAccountByEmail(email, false, isForForgettingPassword);

            if (user != null)
            {
                code = Guid.NewGuid().ToString("N").Substring(0, 6);
                user.VerifyCode = code;
            }

            await _unitOfWork.SaveChangeAsync();

            return code;
        }

        public async Task<AppActionResult> GoogleCallBack(string accessTokenFromGoogle)
        {
            var result = new AppActionResult();
            try
            {
                var existingFirebaseApp = FirebaseApp.DefaultInstance;
                if (existingFirebaseApp == null)
                {
                    var config = Resolve<FirebaseAdminSDK>();
                    var credential = GoogleCredential.FromJson(JsonConvert.SerializeObject(new
                    {
                        type = config!.Type,
                        project_id = config.Project_id,
                        private_key_id = config.Private_key_id,
                        private_key = config.Private_key,
                        client_email = config.Client_email,
                        client_id = config.Client_id,
                        auth_uri = config.Auth_uri,
                        token_uri = config.Token_uri,
                        auth_provider_x509_cert_url = config.Auth_provider_x509_cert_url,
                        client_x509_cert_url = config.Client_x509_cert_url
                    }));
                    var firebaseApp = FirebaseApp.Create(new AppOptions
                    {
                        Credential = credential
                    });
                }

                var verifiedToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(accessTokenFromGoogle);
                var emailClaim = verifiedToken.Claims.FirstOrDefault(c => c.Key == "email");
                var nameClaim = verifiedToken.Claims.FirstOrDefault(c => c.Key == "name");
                var name = nameClaim.Value.ToString();
                var userEmail = emailClaim.Value.ToString();

                if (userEmail != null)
                {
                    var user = await _accountRepository.GetByExpression(a => a!.Email == userEmail && a.IsDeleted == false);
                    if (user == null)
                    {
                        var resultCreate =
                            await CreateAccount(
                                new SignUpRequestDto
                                {
                                    Email = userEmail,
                                    FirstName = name!,
                                    Gender = true,
                                    LastName = string.Empty,
                                    Password = "Google123@",
                                    PhoneNumber = string.Empty
                                }, true);
                        if (resultCreate.IsSuccess)
                        {
                            var account = (Account)resultCreate.Result!;
                            result = await LoginDefault(userEmail, account);
                        }
                    }

                    result = await LoginDefault(userEmail, user);
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        private async Task<AppActionResult> LoginDefault(string email, Account? user)
        {
            var result = new AppActionResult();

            var jwtService = Resolve<IJwtService>();
            var utility = Resolve<Utility>();
            var token = await jwtService!.GenerateAccessToken(new LoginRequestDto { Email = email });

            if (user!.RefreshToken == null)
            {
                user.RefreshToken = jwtService.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = utility!.GetCurrentDateInTimeZone().AddDays(1);
            }

            if (user.RefreshTokenExpiryTime <= utility!.GetCurrentDateInTimeZone())
            {
                user.RefreshTokenExpiryTime = utility.GetCurrentDateInTimeZone().AddDays(1);
                user.RefreshToken = jwtService.GenerateRefreshToken();
            }

            _tokenDto.Token = token;
            _tokenDto.RefreshToken = user.RefreshToken;
			var userRoleRepository = Resolve<IIdentityUserRoleRepository>();
			var roleListDb = await userRoleRepository.GetRoleListByAccountId(user.Id);
			var roleRepository = Resolve<IIdentityRoleRepository>();
			var roleNameList = await roleRepository.GetRoleNameListById(roleListDb);
            if (roleNameList.Contains("ADMIN"))
            {
                _tokenDto.MainRole = "ADMIN";
            }
            else if (roleNameList.Contains("ORGANIZER"))
            {
                _tokenDto.MainRole = "ORGANIZER";
			} else if(roleNameList.Count > 0)
            {
                _tokenDto.MainRole = roleNameList.FirstOrDefault(n => !n.Equals("CUSTOMER"));
            }
            else
            {
                _tokenDto.MainRole = "CUSTOMER";
			}

			result.Result = _tokenDto;
            await _unitOfWork.SaveChangeAsync();

            return result;
        }

        public async Task<AppActionResult> AssignRoleForUserId(string userId, IList<string> roleId)
        {
            var result = new AppActionResult();
            try
            {
                var user = await _accountRepository.GetById(userId);
                var userRoleRepository = Resolve<IIdentityUserRoleRepository>();
                var identityRoleRepository = Resolve<IIdentityRoleRepository>();
                foreach (var role in roleId)
                    if (await identityRoleRepository!.GetById(role) == null)
                        result = BuildAppActionResultError(result, $"Vai trò với id {role} không tồn tại");

                if (!BuildAppActionResultIsError(result))
                    foreach (var role in roleId)
                    {
                        var roleDb = await identityRoleRepository!.GetById(role);
                        var resultCreateRole = await _userManager.AddToRoleAsync(user, roleDb.NormalizedName);
                        if (!resultCreateRole.Succeeded)
                            result = BuildAppActionResultError(result, $"Cấp quyền với vai trò {role} không thành công");
                    }

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> RemoveRoleForUserId(string userId, IList<string> roleId)
        {
            var result = new AppActionResult();

            try
            {
                var user = await _accountRepository.GetById(userId);
                var userRoleRepository = Resolve<IIdentityUserRoleRepository>();
                var identityRoleRepository = Resolve<IIdentityRoleRepository>();
                if (user == null)
                    result = BuildAppActionResultError(result, $"Người dùng với {userId} không tồn tại");
                foreach (var role in roleId)
                    if (await identityRoleRepository.GetById(role) == null)
                        result = BuildAppActionResultError(result, $"Vai trò với {role} không tồn tại");

                if (!BuildAppActionResultIsError(result))
                    foreach (var role in roleId)
                    {
                        var roleDb = await identityRoleRepository!.GetById(role);
                        var resultCreateRole = await _userManager.RemoveFromRoleAsync(user!, roleDb.NormalizedName);
                        if (!resultCreateRole.Succeeded)
                            result = BuildAppActionResultError(result, $"Xóa quyền {role} thất bại");
                    }

                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public void SendAccountCreationEmailForSponsor(List<Account> tourGuideAccountList)
        {
            try
            {
                foreach (var account in tourGuideAccountList)
                {
                    _emailService?.SendEmail(account.Email, $"Account information for sponsor {account.FirstName} {account.LastName} at TicketBooking",
                       TemplateMappingHelper.GetTemplateOTPEmail(TemplateMappingHelper.ContentEmailType.SPONSOR_ACCOUNT_CREATION,
                           $"Username: {account.Email} \nPassword: {SD.DefaultAccountInformation.PASSWORD}\n Vui lòng không chia sẻ thông tin tài khoản của bạn với bất kì ai", $"{account.FirstName} {account.LastName}"));
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<bool> AssignSponsorRole(List<Account> tourGuideAccountList)
        {
            bool isSucess = true;
            try
            {
                var identityRoleRepository = Resolve<IIdentityRoleRepository>();
                var roleDb = await identityRoleRepository!.GetIdentityRoleByName("sponsor");
                foreach (var account in tourGuideAccountList)
                {
                    var accountDb = await _accountRepository.GetByExpression(a => a.PhoneNumber == account.PhoneNumber);
                    if (accountDb != null)
                    {
                        await AssignRoleForUserId(accountDb.Id, new List<string> { roleDb.Id });
                    }
                    else
                    {
                        isSucess = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return isSucess;
        }

        public async Task<AppActionResult> GetAccountsByRoleName(string roleName, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();

            try
            {
                var roleRepository = Resolve<IIdentityRoleRepository>();
                var roleDb = await roleRepository!.GetByExpression(r => r.NormalizedName.Equals(roleName.ToLower()));
                if (roleDb != null)
                {
                    var userRoleRepository = Resolve<IIdentityUserRoleRepository>();
                    var userRoleDb = await userRoleRepository!.GetAllDataByExpression(u => u.RoleId == roleDb.Id, 0, 0, null, false, null);
                    if (userRoleDb.Items != null && userRoleDb.Items.Count > 0)
                    {
                        var accountIds = userRoleDb.Items.Select(u => u.UserId).Distinct().ToList();
                        var accountDb = await _accountRepository.GetAllDataByExpression(a => accountIds.Contains(a.Id), pageNumber, pageSize, null, false, null);
                        result.Result = accountDb;
                    }
                }
                else
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy vai trò {roleName}");
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> GetAccountsByRoleId(Guid Id, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();

            try
            {
                var roleRepository = Resolve<IIdentityRoleRepository>();
                var roleDb = await roleRepository!.GetById(Id);
                if (roleDb != null)
                {
                    var userRoleRepository = Resolve<IIdentityUserRoleRepository>();
                    var userRoleDb = await userRoleRepository!.GetAllDataByExpression(u => u.RoleId == roleDb.Id, 0, 0, null, false, null);
                    if (userRoleDb.Items != null && userRoleDb.Items.Count > 0)
                    {
                        var accountIds = userRoleDb.Items.Select(u => u.UserId).Distinct().ToList();
                        var accountDb = await _accountRepository.GetAllDataByExpression(a => accountIds.Contains(a.Id), pageNumber, pageSize, null, false, null);
                        result.Result = accountDb;
                    }
                }
                else
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy vai trò với id {Id}");
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }

            return result;
        }

        public async Task<AppActionResult> AddSponsor(CreateSponsorDto dto)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var sponsorRepository = Resolve<ISponsorRepository>();
                var sponsorDb = await sponsorRepository.CreateSponsor(dto);
				var sponsor = _mapper.Map<Account>(dto);
				sponsor.Id = Guid.NewGuid().ToString();
				sponsor.UserName = dto.Email;
				sponsor.IsDeleted = false;
				sponsor.IsVerified = true;
				sponsor.VerifyCode = null;
				string pathName = SD.FirebasePathName.SPONSOR_PREFIX + sponsor.Id;
				var url = await _firebaseService.UploadFileToFirebase(dto.Img, pathName);
                if(url.IsSuccess) {
					sponsorDb.Img = (string)url.Result;
				} else
                {
					result = BuildAppActionResultError(result, $"Tải hình nhà tài trợ thất bại, vui lòng thử lại");
                    return result;
				}
				var resultCreateUser = await _userManager.CreateAsync(sponsor, SD.DefaultAccountInformation.PASSWORD);
				if (!resultCreateUser.Succeeded)
				{
					result = BuildAppActionResultError(result, $"Creation of the account for sponsor with name {dto.Name} failed.");
                    return result;
				}
				bool isSuccessful = await AssignSponsorRole(new List<Account> { sponsor });
                if (isSuccessful)
                {
                    await sponsorRepository.Insert(sponsorDb);
                    await _unitOfWork.SaveChangeAsync();
                    SendAccountCreationEmailForSponsor(new List<Account> { sponsor });
                    result.Result = sponsor;
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GenerateQR(string Id)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var accountDb = await _accountRepository.GetById(Id);
                if (accountDb == null)
                {
                    result = BuildAppActionResultError(result, "Không tìm thấy thông tin tài khoản");
                    return result;
                }
                string qrAccountString = $"{accountDb.FirstName} {accountDb.LastName},{accountDb.PhoneNumber},{accountDb.Email}";
                //string encryptAccountResponseString = EncryptData(qrAccountString, SD.QR_CODE_KEY);
                string pathName = SD.FirebasePathName.QR_PREFIX + accountDb.Id;
                IFormFile qr = GenerateQRCodeImage(qrAccountString);
                var url = await _firebaseService.UploadFileToFirebase(qr, pathName);
                if (url.IsSuccess)
                {
                    accountDb.qr = url.Result!.ToString();
                    result.Messages.Add(accountDb.qr);
                    //await _accountRepository.Update(accountDb);
                    await _unitOfWork.SaveChangeAsync();
                }

            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        private string EncryptData(string data, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.IV = new byte[16]; // Assuming a zero IV for simplicity
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new System.IO.StreamWriter(cs))
                        {
                            sw.Write(data);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }


        public IFormFile GenerateQRCodeImage(string data)
        {
            GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(data, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium);

            // Save barcode as PNG in memory
            byte[] barcodeBytes = barcode.ToPngBinaryData();

            // Create a MemoryStream from the barcode bytes
            MemoryStream ms = new MemoryStream(barcodeBytes);

            // Create an IFormFile from the MemoryStream
            IFormFile formFile = new FormFile(ms, 0, ms.Length, "barcode.png", "image/png");

            // Set the position of the MemoryStream back to the beginning for subsequent reads
            ms.Position = 0;

            return formFile;
        }

        public async Task<AppActionResult> DecodeQR(string hashedAccountData)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                //string decryptData = DecryptData(hashedAccountData, SD.QR_CODE_KEY);
                //if(decryptData != null)
                //{
                //}
                string[] data = hashedAccountData.Split(',');
                result.Result = new QRAccountResponse
                {
                    FullName = data[0],
                    PhoneNumber = data[1],
                    Email = data[2]
                };
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        private string DecryptData(string encryptedData, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = new byte[16];
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;// Assuming a zero IV for simplicity
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(Convert.FromBase64String(encryptedData)))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

		public async Task<AppActionResult> AssignRole(string userId, string roleName)
		{
			AppActionResult result = new AppActionResult();
			try
			{
                var accountDb = await _accountRepository.GetById(userId);
                if(accountDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tìm thấy tài khoản với id {userId}");
                    return result;
                }
                var roleRepository = Resolve<IIdentityRoleRepository>();

                var roleDb = await roleRepository.GetIdentityRoleByName(roleName);
                if(roleDb == null)
                {
					result = BuildAppActionResultError(result, $"Không tìm thấy phân quyền với tên {roleName}");
					return result;
				}

				var userRoleRepository = Resolve<IIdentityUserRoleRepository>();
                var roleListDb = await userRoleRepository.GetRoleListByAccountId(userId);
                if(roleListDb.Count() != 0) {
                    if (roleListDb.Contains(roleDb.Id))
                    {
						result = BuildAppActionResultError(result, $"Tài khoản với id {userId} đã có phân quyền {roleName}");
						return result;
					}
                }

                bool isSuccessful = await userRoleRepository.AssignRole(userId, roleDb.Id);
                if (!isSuccessful)
                {
					result = BuildAppActionResultError(result, $"Thêm phân quyền không thành công, vui lòng thử lại sau");
					return result;
				}
			}
			catch (Exception ex)
			{
				result = BuildAppActionResultError(result, ex.Message);
			}
			return result;
		}
	}
}