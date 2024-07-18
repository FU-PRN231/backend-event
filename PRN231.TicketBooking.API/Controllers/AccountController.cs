using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("create-account")]
        public async Task<AppActionResult> CreateAccount(SignUpRequestDto request)
        {
            return await _accountService.CreateAccount(request, false);
        }

        [HttpGet("get-all-account")]
        [Authorize("ADMIN")]
        public async Task<AppActionResult> GetAllAccount(int pageIndex = 1, int pageSize = 10)
        {
            return await _accountService.GetAllAccount(pageIndex, pageSize);
        }

        [HttpGet("get-accounts-by-role-name/{roleName}/{pageIndex:int}/{pageSize:int}")]
        public async Task<AppActionResult> GetAccountsByRoleName(string roleName, int pageIndex = 1, int pageSize = 10)
        {
            return await _accountService.GetAccountsByRoleName(roleName, pageIndex, pageSize);
        }

        [HttpPost("login")]
        public async Task<AppActionResult> Login(LoginRequestDto request)
        {
            return await _accountService.Login(request);
        }

        [HttpGet("get-accounts-by-role-id/{roleId}/{pageIndex:int}/{pageSize:int}")]
        public async Task<AppActionResult> GetAccountsByRoleId(Guid roleId, int pageIndex = 1, int pageSize = 10)
        {
            return await _accountService.GetAccountsByRoleId(roleId, pageIndex, pageSize);
        }

        [HttpPut("update-account")]
        public async Task<AppActionResult> UpdateAccount(UpdateAccountRequestDto request)
        {
            return await _accountService.UpdateAccount(request);
        }

        [HttpPost("get-account-by-userId/{id}")]
        [Authorize("REGISTERED")]
        public async Task<AppActionResult> GetAccountByUserId(string id)
        {
            return await _accountService.GetAccountByUserId(id);
        }

        [HttpPut("change-password")]
        [Authorize("REGISTERED")]
        public async Task<AppActionResult> ChangePassword(ChangePasswordDto dto)
        {
            return await _accountService.ChangePassword(dto);
        }

        [HttpPost("get-new-token/{userId}")]
        public async Task<AppActionResult> GetNewToken([FromBody] string refreshToken, string userId)
        {
            return await _accountService.GetNewToken(refreshToken, userId);
        }

        [HttpPut("forgot-password")]
        [Authorize("REGISTERED")]
        public async Task<AppActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            return await _accountService.ForgotPassword(dto);
        }

        [HttpPut("active-account/{email}/{verifyCode}")]
        public async Task<AppActionResult> ActiveAccount(string email, string verifyCode)
        {
            return await _accountService.ActiveAccount(email, verifyCode);
        }

        [HttpPost("send-email-forgot-password/{email}")]
        public async Task<AppActionResult> SendEmailForgotPassword(string email)
        {
            return await _accountService.SendEmailForgotPassword(email);
        }

        [HttpPost("send-email-for-activeCode/{email}")]
        public async Task<AppActionResult> SendEmailForActiveCode(string email)
        {
            return await _accountService.SendEmailForActiveCode(email);
        }

        [HttpPost("google-callback")]
        public async Task<AppActionResult> GoogleCallBack([FromBody] string accessTokenFromGoogle)
        {
            return await _accountService.GoogleCallBack(accessTokenFromGoogle);
        }

        [HttpGet("generate-account-qr-code/{accountId}")]
        public async Task<AppActionResult> GenerateQR(string accountId)
        {
            return await _accountService.GenerateQR(accountId);
        }

        [HttpGet("decode-qr/{qrString}")]
        public async Task<AppActionResult> DecodeQR(string qrString)
        {
            return await _accountService.DecodeQR(qrString);
        }

		[HttpPost("assign-role")]
        [Authorize("ADMIN")]
        public async Task<AppActionResult> AssignRole(string userId, string roleName)
		{
			return await _accountService.AssignRole(userId, roleName);
		}

		[HttpPost("add-sponsor")]
        [Authorize("ADMIN")]
        public async Task<AppActionResult> AddSponsor([FromForm]CreateSponsorDto dto)
		{
			return await _accountService.AddSponsor(dto);
		}
	}
}