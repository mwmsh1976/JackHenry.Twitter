using System.Collections.Generic;
using Newtonsoft.Json;

namespace JackHenry.Twitter.Tweets
{
    public class Tweets
    {
        [JsonProperty("data")]
        public List<Tweet> Data { get; set; }
    }
}
