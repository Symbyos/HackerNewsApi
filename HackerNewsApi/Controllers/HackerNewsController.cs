using HackerNewsApi.Models;
using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsApiService _hackerNewsApiService;

        private readonly ILogger<HackerNewsController> _logger;

        public HackerNewsController(ILogger<HackerNewsController> logger, IHackerNewsApiService hackerNewsServiceApi)
        {
            _logger = logger;
            _hackerNewsApiService = hackerNewsServiceApi;
        }

        [HttpGet(Name = "GetBestNewsByScoreDescending")]
        async public Task<IList<News>> GetBestNews()
        {
            return await _hackerNewsApiService.GetBestNewsByScoreDescending();
        }
    }
}