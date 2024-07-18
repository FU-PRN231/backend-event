using Humanizer;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.X509.Qualified;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class PostService : GenericBackendService, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IServiceProvider serviceProvider,
            IPostRepository postRepository, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppActionResult> CreatePostforEvent(CreatePostDto createPostDto)
        {
            var result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetEventById(createPostDto.EventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tồn tại sự kiện với Id {createPostDto.EventId}");
                    return result;
                }
                var post = new Post
                {
                    Content = createPostDto.Content,
                    Id = Guid.NewGuid(),
                    Title = createPostDto.Title,
                    EventId = createPostDto.EventId,
                };
                await _postRepository.Insert(post);
                await _unitOfWork.SaveChangeAsync();
                result.Messages.Add("Tạo mới bài viết thành công");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> DeletePost(Guid postId)
        {
            var result = new AppActionResult();
            try
            {
                var postDb = await _postRepository.GetById(postId);
                if (postDb == null)
                {
                    result = BuildAppActionResultError(result, $"Bài post này khong tìm thấy với id {postId}");
                    return result;
                }
                await _postRepository.DeleteById(postId);
                await _unitOfWork.SaveChangeAsync();
                result.Messages.Add("Xóa bài viết thành công");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllPost(int pageNumeber, int pageSize)
        {
            var result = new AppActionResult();
            try
            {
                result.Result = await _postRepository.GetAllPost(null, pageNumeber, pageSize, null, false, p => p.Event!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllPostOfEvent(Guid eventId, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository!.GetById(eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Không tồn tại sự kiện với Id {eventId}");
                    return result;
                }
                var postDb = await _postRepository.GetAllPost(p => p.EventId == eventId, pageNumber, pageSize, null, false, p => p.Event!);
                result.Result = postDb;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetPostById(Guid postId)
        {
            var result = new AppActionResult();
            try
            {
                result.Result = await _postRepository.GetById(postId);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> UpdatePost(UpdatePostDto updatePostDto)
        {
            var result = new AppActionResult();
            try
            {
                var postDb = await _postRepository.GetById(updatePostDto.Id);
                if (postDb == null)
                {
                    result = BuildAppActionResultError(result, $"Bài post này khong tìm thấy với id {updatePostDto.Id}");
                    return result;
                }
                postDb.Title = updatePostDto.Title; 
                postDb.Content = updatePostDto.Content;
                await _unitOfWork.SaveChangeAsync();
                result.Messages.Add("Cập nhập bài post thành công");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
