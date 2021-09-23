using System.Threading.Tasks;

namespace JackHenry.Twitter.Interfaces
{
    public interface ITwitterStream
    {
        Task StreamTweetsAsync();
        double TotalMinutes { get; }
        long TweetCount { get; }
        double AverageTweetsPerMinute { get; }
    }
}
