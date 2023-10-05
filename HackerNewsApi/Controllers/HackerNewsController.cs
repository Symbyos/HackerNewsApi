using HackerNewsApi.Models;
using HackerNewsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsServiceApi _hackerNewsService;

        private readonly ILogger<HackerNewsController> _logger;

        public HackerNewsController(ILogger<HackerNewsController> logger, IHackerNewsServiceApi hackerNewsServiceApi)
        {
            _logger = logger;
            _hackerNewsService = hackerNewsServiceApi;
        }

        [HttpGet(Name = "GetBestNews")]
        async public Task<IList<News>> GetBestNews()
        {
            return await _hackerNewsService.GetBestNewsIdsByScoreDescending();
        }
    }
}