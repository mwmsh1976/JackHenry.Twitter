using JackHenry.Twitter.Custom_Configuration;
using JackHenry.Twitter.Rules;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace JackHenry.Twitter.Tests
{
    public class DummyData
    {
        private static readonly Lazy<DummyData> _instance =
            new(() => new DummyData());

        public static DummyData Instance
        {
            get { return _instance.Value; }
        }

        private DummyData() { }

        public IOptions<RuleConfiguration> RuleConfig
        {
            get{
                return Options.Create(new RuleConfiguration()
                {
                    RuleEndpoint = "http://jackhenry.com",
                    RuleTag = "NFL Tag",
                    RuleValue = "Franchise"
                });
            }
        }

        public AddRule AddRule
        {
            get
            {
                List<Rule> ruleList = new List<Rule>
            {
                new Rule
                {
                    Value = "RuleValue",
                    Tag = "RuleTag"
                }
            };
            var rules = new AddRule
            {
                Add = ruleList
            };
            return rules;
            }

         }

        public RuleResults RuleResults
        {
            get
            {
                var ruleList = new List<RuleResult>();
                var ruleResults = new RuleResults();
                ruleResults.Data = ruleList;
                return ruleResults;
            }
        }
    }
}
