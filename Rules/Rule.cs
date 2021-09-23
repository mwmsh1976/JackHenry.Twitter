
using Newtonsoft.Json;

namespace JackHenry.Twitter.Rules
{
    public class Rule
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("tag")]
        public string Tag { get; set; }
    }
}
