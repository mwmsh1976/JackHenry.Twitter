using JackHenry.Twitter.Custom_Configuration;
using JackHenry.Twitter.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace JackHenry.Twitter
{
    public class StartupExtension
    {
        private readonly IServiceProvider _services;
        private ITwitterStream _twitterService;
        private ITwitterRuleManager _twitterRuleManager;
        private Timer _timer = null;

        private  readonly IOptions<SharedConfiguration> _sharedConfig;

        public StartupExtension(IServiceProvider services, IOptions<SharedConfiguration> sharedConfig)
        {
            _services = services;
            _sharedConfig = sharedConfig;
        }

        public async Task<bool> InitializeApplication()
        {
            _twitterService = _services.GetService(typeof(ITwitterStream)) as TwitterStream;
            _twitterRuleManager = _services.GetService(typeof(ITwitterRuleManager)) as TwitterRuleManager;

            if (await _twitterRuleManager.SetRules())
            {
                SetTimer();
                await _twitterService.StreamTweetsAsync();
            }
            return true;
        }

        private void SetTimer()
        {
            _timer = new Timer(_sharedConfig.Value.TimerFireInMilliseconds);
            _timer.Elapsed += TimerFired;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void TimerFired(object source, ElapsedEventArgs e)
        {
            if(_twitterService.TweetCount > 0)
            {
                Console.WriteLine("Total Minutes:  " + _twitterService.TotalMinutes.ToString() + Environment.NewLine +
                                  "TweetCount:     " + _twitterService.TweetCount.ToString() + Environment.NewLine +
                                  "Avg Tweets/min: " + _twitterService.AverageTweetsPerMinute + Environment.NewLine);
            }
            
        }
    }
}
