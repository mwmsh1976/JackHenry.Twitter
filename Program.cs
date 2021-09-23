using JackHenry.Api.Core;
using JackHenry.Api.Core.Interfaces;
using JackHenry.Twitter;
using JackHenry.Twitter.Custom_Configuration;
using JackHenry.Twitter.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    private const string SETTINGS_JSON = "appsettings.json";

    private static string BEARER_TOKEN_NAME = "TwitterBearerToken";
    private static string BEARER_TOKEN_HEADER_NAME = "TwitterBearerTokenKeyName"; 
    private static string _bearerToken = "";
    private static string _bearerTokenHeaderName = "";

    static async Task<int> Main(string[] args)
    {             
        try
        {
            _bearerToken = Environment.GetEnvironmentVariable(BEARER_TOKEN_NAME);
            _bearerTokenHeaderName = Environment.GetEnvironmentVariable(BEARER_TOKEN_HEADER_NAME);
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<StartupExtension>().InitializeApplication();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Whoops! Something went wrong." + Environment.NewLine +
                ex.Message);
        }

        return 0;
    }

    private static IServiceCollection ConfigureServices()
    {
        IServiceCollection services = new ServiceCollection();

        var configuration = LoadConfiguration();

        services.AddOptions();

        services.Configure<SharedConfiguration>(configuration.GetSection("Twitter:Shared"));
        services.Configure<StreamConfiguration>(configuration.GetSection("Twitter:Stream"));
        services.Configure<RuleConfiguration>(configuration.GetSection("Twitter:Rules"));
        
        var sharedSettings = new SharedConfiguration();
        configuration.GetSection("Twitter:Shared").Bind(sharedSettings);

        services.AddTransient<StartupExtension>();
        services.AddHttpClient(sharedSettings.ClientName, client =>
        {
            client.BaseAddress = new Uri(sharedSettings.BaseApiAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add(_bearerTokenHeaderName, _bearerToken);
            client.DefaultRequestHeaders.Add("User-Agent", "v2SampleStreamCSharp");
        });

        services.AddSingleton<IServiceClient, ServiceClient>(s =>
                     new ServiceClient(s.GetService<IHttpClientFactory>(), sharedSettings.ClientName)
                     );
        
        services.AddTransient<ITwitterStream, TwitterStream>();
        services.AddTransient<ITwitterRuleManager, TwitterRuleManager>();
        return services;
    }

    public static IConfiguration LoadConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(SETTINGS_JSON, optional: true, reloadOnChange: true);

        return builder.Build();
    }
}
