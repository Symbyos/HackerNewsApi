using System.Text;
using System.Text.Json.Serialization;

namespace HackerNewsApi.Models
{
    /// <summary>
    /// Represents a news as represented by the HackerNews API
    /// </summary>
    public record class HackerNews
    {
        [JsonPropertyName("by")]
        public string Author { get; set; }

        [JsonPropertyName("descendants")]
        public int CommentCount { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("kids")]
        public List<int> CommentIds { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("time")]
        public int UnixTimestamp { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        public HackerNews()
        {
            Author = string.Empty;
            CommentIds = new List<int>();
            Title = string.Empty;
            Type = string.Empty;
            Url = string.Empty;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            

            return stringBuilder.ToString();
        }
    }
}
