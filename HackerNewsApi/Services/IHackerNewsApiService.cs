using HackerNewsApi.Models;

namespace HackerNewsApi.Services
{
    public interface IHackerNewsApiService
    {
        Task<IList<News>> GetBestNewsByScoreDescending();
    }
}
