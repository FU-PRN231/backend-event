using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
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

        [HttpGet("get-all-post/{pageNumber}/{pageSize}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetAllPost(int pageNumber = 1, int pageSize = 10)
        {
            return await _postService.GetAllPost(pageNumber, pageSize);
        }

        [HttpGet("get-all-post-of-event/{eventId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetAllPostOfEvent(Guid eventId, int pageNumber = 1, int pageSize = 10)
        {
            return await _postService.GetAllPostOfEvent(eventId, pageNumber, pageSize);
        }

        [HttpGet("get-post-by-id/{postId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetPostById(Guid postId)
        {
            return await _postService.GetPostById(postId);  
        }

        [HttpPost("create-post-for-event")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> CreatePostforEvent(CreatePostDto createPostDto)
        {
            return await _postService.CreatePostforEvent(createPostDto);    
        }

        [HttpPut("update-post")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> UpdatePost(UpdatePostDto updatePostDto)
        {
            return await _postService.UpdatePost(updatePostDto);
        }

        [HttpDelete("delete-post")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult>  DeletePost(Guid postId)
        {
            return await _postService.DeletePost(postId);   
        }
    }
}
