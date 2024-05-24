using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.Common.Dto;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IFirebaseService
    {
        Task<AppActionResult> UploadFileToFirebase(IFormFile file, string pathFileName);

        public Task<string> GetUrlImageFromFirebase(string pathFileName);

        public Task<AppActionResult> DeleteFileFromFirebase(string pathFileName);
    }
}