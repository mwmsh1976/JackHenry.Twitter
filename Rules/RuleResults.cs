using Newtonsoft.Json;
using System.Collections.Generic;

namespace JackHenry.Twitter.Rules
{
    public class RuleResults
    {
        [JsonProperty("data")]
        public List<RuleResult> Data { get; set; }
    }
}
