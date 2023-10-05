﻿using System.Text.Json.Serialization;

namespace HackerNewsApi.Models
{
    /// <summary>
    /// Represents a news
    /// </summary>
    public class News
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("postedBy")]
        public string Author { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("descendants")]
        public int CommentCount { get; set; }

        public News()
        {
            Author = string.Empty;
            Title = string.Empty;
            Uri = string.Empty;
        }

        public News(HackerNews hackerNews)
        {
            Title = hackerNews.Title;
            Uri = hackerNews.Url;
            Author = hackerNews.Author;
            Time = DateTimeOffset.FromUnixTimeSeconds(hackerNews.UnixTimestamp).DateTime;
            Score = hackerNews.Score;
            CommentCount = hackerNews.CommentCount;
        }
    }
}