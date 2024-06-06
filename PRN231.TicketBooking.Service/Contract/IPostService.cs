using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IPostService
    {
        Task<AppActionResult> CreatePostforEvent(CreatePostDto createPostDto);
        Task<AppActionResult> UpdatePostforEvent(UpdatePostDto updatePostDto);
        Task<AppActionResult> GetAllPostOfEvent(Guid eventId, int pageNumber, int pageSize);
        Task<AppActionResult> GetPostById(Guid postId);
        Task<AppActionResult> DeletePost(Guid postId);
    }
}
