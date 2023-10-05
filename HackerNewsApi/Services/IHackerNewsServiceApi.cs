using HackerNewsApi.Models;

namespace HackerNewsApi.Services
{
    public interface IHackerNewsServiceApi
    {
        Task<IList<News>> GetBestNewsByScoreDescending();
    }
}
