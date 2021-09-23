using System.Threading.Tasks;

namespace JackHenry.Twitter.Interfaces
{
    public interface ITwitterRuleManager
    {
        Task<bool> SetRules();
    }
}
