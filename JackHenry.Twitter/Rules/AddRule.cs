using Newtonsoft.Json;
using System.Collections.Generic;

namespace JackHenry.Twitter.Rules
{
    public class AddRule
    {
        [JsonProperty("add")]
        public List<Rule> Add { get; set; }
    }
}
