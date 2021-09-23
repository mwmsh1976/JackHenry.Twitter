using Newtonsoft.Json;
using System;

namespace JackHenry.Twitter.Tweets
{
    public class Tweet
    {
        [JsonProperty("author_id")]
        public string Author_Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created_At { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
