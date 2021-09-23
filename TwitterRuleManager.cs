using JackHenry.Api.Core.Interfaces;
using JackHenry.Twitter.Custom_Configuration;
using JackHenry.Twitter.Interfaces;
using JackHenry.Twitter.Rules;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JackHenry.Twitter
{
    public class TwitterRuleManager : ITwitterRuleManager
    {

        private readonly IServiceClient _serviceClient;
        private readonly IOptions<RuleConfiguration> _ruleConfig;

        public TwitterRuleManager(IServiceClient serviceClient, IHttpClientFactory httpClientFactory,
                                  IOptions<RuleConfiguration> ruleConfig)
        {
            _serviceClient = serviceClient;
            _ruleConfig = ruleConfig;
        }

        public async Task<bool> SetRules()
        {
            List<Rule> ruleList = new List<Rule>
            {
                new Rule
                {
                    Value = _ruleConfig.Value.RuleValue,
                    Tag = _ruleConfig.Value.RuleTag
                }
            };
            var rules = new AddRule
            {
                Add = ruleList
            };
            try
            {
                if (!await RuleExists())
                {
                    var ruleContent = new StringContent(JsonConvert.SerializeObject(rules), Encoding.UTF8, "application/json");
                    await _serviceClient.PostAsync<RuleResults>(_ruleConfig.Value.RuleEndpoint, ruleContent);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        private async Task<bool> RuleExists()
        {
            var result = await _serviceClient.GetAsync<RuleResults>(_ruleConfig.Value.RuleEndpoint);

            if (result != null && result.Data.Any())
            {
                return result.Data.Any(r => (r.Tag.ToLower() == _ruleConfig.Value.RuleTag.ToLower() &&
                    r.Value.ToLower() == _ruleConfig.Value.RuleValue.ToLower()));
            }

            return false;
        }
    }
}
