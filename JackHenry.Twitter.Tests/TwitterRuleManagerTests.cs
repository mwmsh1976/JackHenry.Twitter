using JackHenry.Api.Core.Interfaces;
using JackHenry.Twitter.Custom_Configuration;
using JackHenry.Twitter.Rules;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Xunit;

namespace JackHenry.Twitter.Tests
{
    public class TwitterRuleManagerTests
    {
        private readonly DummyData _dummyData;

        public TwitterRuleManagerTests()
        {
            _dummyData = DummyData.Instance;
        }

        [Fact]
        public async void SetRules_NullCurrentRules_Should_Pass()
        {
            //Arrange            
            IOptions<RuleConfiguration> ruleOptions = _dummyData.RuleConfig;
            var mockedServiceClient = new Mock<IServiceClient>();
            var mockedHttpClientFactory = new Mock<IHttpClientFactory>();
            var httpContent =  new StringContent(JsonConvert.SerializeObject(_dummyData.AddRule), 
                Encoding.UTF8, "application/json");
            mockedServiceClient.Setup(s => s.GetAsync<RuleResults>(It.IsAny<string>()))
                .ReturnsAsync(_dummyData.EmptyRuleResults);
            mockedServiceClient.Setup(s => s.PostAsync<RuleResults>(It.IsAny<string>(), httpContent))
                .ReturnsAsync(_dummyData.RuleResults);
            var twitterRuleManager = new TwitterRuleManager(mockedServiceClient.Object, mockedHttpClientFactory.Object, 
                ruleOptions);

            //Act
            var callResult = await twitterRuleManager.SetRules();

            //Assert
            Assert.True(callResult == true);
        }

        [Fact]
        public async void SetRules_CurrentRulesExist_Should_Pass()
        {
            //Arrange            
            IOptions<RuleConfiguration> ruleOptions = _dummyData.RuleConfig;
            var mockedServiceClient = new Mock<IServiceClient>();
            var mockedHttpClientFactory = new Mock<IHttpClientFactory>();
            var httpContent = new StringContent(JsonConvert.SerializeObject(_dummyData.AddRule),
                Encoding.UTF8, "application/json");
            mockedServiceClient.Setup(s => s.GetAsync<RuleResults>(It.IsAny<string>()))
                .ReturnsAsync(_dummyData.RuleResults);
            mockedServiceClient.Setup(s => s.PostAsync<RuleResults>(It.IsAny<string>(), httpContent))
                .ReturnsAsync(_dummyData.RuleResults);
            var twitterRuleManager = new TwitterRuleManager(mockedServiceClient.Object, mockedHttpClientFactory.Object,
                ruleOptions);

            //Act
            var callResult = await twitterRuleManager.SetRules();

            //Assert
            Assert.True(callResult == true);
        }
    }
}
