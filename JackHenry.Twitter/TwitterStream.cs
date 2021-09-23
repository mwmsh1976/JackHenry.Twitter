using JackHenry.Twitter.Custom_Configuration;
using JackHenry.Twitter.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JackHenry.Twitter
{
    public class TwitterStream : ITwitterStream
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<StreamConfiguration> _streamConfig;
        private readonly IOptions<SharedConfiguration> _sharedConfig;
        private HttpClient _client;

        public TwitterStream(IHttpClientFactory httpClientFactory, IOptions<StreamConfiguration> streamConfig,
                             IOptions<SharedConfiguration> sharedConfig)
        {
            _httpClientFactory = httpClientFactory;
            _streamConfig = streamConfig;
            _sharedConfig = sharedConfig;
        }

        private Stopwatch _stopwatch = new Stopwatch();

        public int ElapsedSeconds { get; private set; }
        public long TweetCount { get; private set; }
        public double AverageTweetsPerMinute { get => TweetCount / TotalMinutes; }
        public double TotalMinutes { get => _stopwatch.Elapsed.TotalMinutes; }
        public long SecondsRunning { get; private set; }

        
        public async Task StreamTweetsAsync()
        {
            _client = _httpClientFactory.CreateClient(_sharedConfig.Value.ClientName);
            _client.Timeout = new TimeSpan(0, 0, 0, 10, 0);

            try
            {
                string resultLine = "";

                Stream twitterStream = await _client.GetStreamAsync(_streamConfig.Value.StreamEndpoint);

                using StreamReader reader = new StreamReader(twitterStream, Encoding.ASCII, false);

                _stopwatch.Start();

                while ((resultLine = reader.ReadLine()) != null)
                {
                    Thread.Sleep(250);
                    TweetCount += 1;
                    ElapsedSeconds = (int)(_stopwatch.ElapsedMilliseconds * 1000);
                }
            }
            catch (Exception ex)
            {               
                throw new Exception(ex.Message);
            }
            finally
            {
                _stopwatch.Stop();
            }
        }

    }
}