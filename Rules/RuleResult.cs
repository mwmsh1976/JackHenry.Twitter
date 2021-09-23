using Newtonsoft.Json;

namespace JackHenry.Twitter.Rules
{
    public class RuleResult
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("tag")]
        public string Tag { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
