using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("get-all-post-of-event/{eventId}")]
        public async Task<AppActionResult> GetAllPostOfEvent(Guid eventId, int pageNumber = 1, int pageSize = 10)
        {
            return await _postService.GetAllPostOfEvent(eventId, pageNumber, pageSize);
        }

        [HttpGet("get-post-by-id/{postId}")]
        public async Task<AppActionResult> GetPostById(Guid postId)
        {
            return await _postService.GetPostById(postId);  
        }

        [HttpPost("create-post-for-event")]
        public async Task<AppActionResult> CreatePostforEvent(CreatePostDto createPostDto)
        {
            return await _postService.CreatePostforEvent(createPostDto);    
        }

        [HttpPut("update-post")]
        public async Task<AppActionResult> UpdatePost(UpdatePostDto updatePostDto)
        {
            return await _postService.UpdatePost(updatePostDto);
        }

        [HttpDelete("delete-post")]
        public async Task<AppActionResult>  DeletePost(Guid postId)
        {
            return await _postService.DeletePost(postId);   
        }
    }
}
