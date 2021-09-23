
using System.Configuration;

namespace JackHenry.Twitter.Custom_Configuration
{
    public class RuleConfiguration : ConfigurationSection
    {
        public string RuleEndpoint { get; set; }
        public string RuleTag { get; set; }
        public string RuleValue { get; set; }
    }
}
