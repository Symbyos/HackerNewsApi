using HackerNewsApi.Models;
using System.Text.Json;
using HackerNewsApi.Services;

namespace HackerNewsApi.Handlers
{
    public class HackerNewsServiceApi : IHackerNewsServiceApi
    {
        static readonly string URL_BEST_STORIES = "https://hacker-news.firebaseio.com/v0/beststories.json";

        static readonly string URL_STORY = "https://hacker-news.firebaseio.com/v0/item/{STORY_ID}.json";

        static readonly string STORY_ID = "{STORY_ID}";

        static readonly IDictionary<int, HackerNews> hackerNewsCache = new Dictionary<int, HackerNews>();

        HttpClient HttpClient { get; }

        public HackerNewsServiceApi()
        {
            HttpClient = new HttpClient();
        }

        async public Task<IList<News>> GetBestNewsByScoreDescending()
        {
            return (await GetBestNewsIds()).Select(x => new News(x)).OrderByDescending(x => x.Score).ToList();
        }

        async public Task<IList<HackerNews>> GetBestNewsIds()
        {
            IList<HackerNews> bestNews = new List<HackerNews>();
            foreach(int id in await queryBestNewsIds())
            {
                bestNews.Add(await GetNews(id));
            }

            return bestNews;
        }

        async public Task<HackerNews> GetNews(int id)
        {
            if (hackerNewsCache.ContainsKey(id))
            {
                return hackerNewsCache[id];
            }

            return await queryNews(id);
        }

        async private Task<IList<int>> queryBestNewsIds()
        {
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(URL_BEST_STORIES);
            if(httpResponseMessage.IsSuccessStatusCode)
            {
                using (Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync())
                {
                    IList<int> bestNewsIds = await JsonSerializer.DeserializeAsync<List<int>>(contentStream) ?? new List<int>();

                    return bestNewsIds;
                }
            }
            else
            {
                throw new Exception($"Unable to query {URL_BEST_STORIES}.");
            }
        }

        async private Task<HackerNews> queryNews(int id)
        {
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(URL_STORY.Replace(STORY_ID, id.ToString()));
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using (Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync())
                {
                    HackerNews news = await JsonSerializer.DeserializeAsync<HackerNews>(contentStream) ?? new HackerNews();

                    // Using TryAdd in place of Add, in case multiple threads are trying to write the same key
                    // Using a lock would slow down the application, the tradeoff would not be interesting enough to use a synchronization primitive
                    hackerNewsCache.TryAdd(news.Id, news);

                    return news;
                }
            }
            else
            {
                throw new Exception($"Unable to query {URL_STORY} for STORY_ID {id}.");
            }
        }
    }
}
