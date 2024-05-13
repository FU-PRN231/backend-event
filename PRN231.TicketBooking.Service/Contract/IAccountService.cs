using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IAccountService
    {
        Task<AppActionResult> Login(LoginRequestDto loginRequest);

        public Task<AppActionResult> VerifyLoginGoogle(string email, string verifyCode);

        Task<AppActionResult> CreateAccount(SignUpRequestDto signUpRequest, bool isGoogle);

        Task<AppActionResult> UpdateAccount(UpdateAccountRequestDto applicationUser);

        Task<AppActionResult> ChangePassword(ChangePasswordDto changePasswordDto);

        Task<AppActionResult> GetAccountByUserId(string id);

        Task<AppActionResult> GetAllAccount(int pageIndex, int pageSize);

        Task<AppActionResult> GetNewToken(string refreshToken, string userId);

        Task<AppActionResult> ForgotPassword(ForgotPasswordDto dto);

        Task<AppActionResult> ActiveAccount(string email, string verifyCode);

        Task<AppActionResult> SendEmailForgotPassword(string email);

        Task<string> GenerateVerifyCode(string email, bool isForForgettingPassword);

        Task<AppActionResult> GoogleCallBack(string accessTokenFromGoogle);

        public Task<AppActionResult> SendEmailForActiveCode(string email);
        public Task<AppActionResult> GetAccountsByRoleName(string roleName, int pageNumber, int pageSize);
        public Task<AppActionResult> GetAccountsByRoleId(Guid Id, int pageNumber, int pageSize);
        public Task<AppActionResult> AddSponsor(CreateSponsorDto dto);
    }
}
