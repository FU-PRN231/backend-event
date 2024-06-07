using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using PRN231.TicketBooking.Common.ConfigurationModel;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("cache")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly RedisConfiguration _cacheConfiguration;
        public readonly IResponseCacheService _responseCacheService;

        public CacheController(IDistributedCache distributedCache, RedisConfiguration cacheConfiguration, IResponseCacheService responseCacheService)
        {
            _cacheConfiguration = cacheConfiguration;
            _responseCacheService = responseCacheService;
        }

        [HttpPost("clear-cache")]
        public async Task<IActionResult> ClearCacheAsync()
        {
            if (!_cacheConfiguration.Enabled)
            {
                return Ok();
            }

            var cacheKey = "/";
            await _responseCacheService.RemoveCacheResponseAsync(cacheKey);
            return Ok();
        }
    }
}
