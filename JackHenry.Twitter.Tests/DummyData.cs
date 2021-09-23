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

        public string Id { get => "12345"; }
        public string Tag { get => "NFL Tag"; }
        public string Value { get => "Franchise"; }

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
                ruleList.Add(new RuleResult
                {
                    Id = Id.ToString(),
                    Tag = Tag,
                    Value = Value
                });
                var ruleResults = new RuleResults();
                ruleResults.Data = ruleList;
                return ruleResults;
            }
        }
        public RuleResults EmptyRuleResults
        {
            get
            {
                return null;
            }
        }
    }
}
