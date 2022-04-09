using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramChannelAnalyzer.Configuration;
using TelegramChannelAnalyzer.Services;

namespace TelegramChannelAnalyzer
{
    public static class Bootstrapper
    {      
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.Sources.Clear();

                configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false);         
            });
            return host;
        }     

        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((hostingContext, services) =>
            {
                var configurationRoot = hostingContext.Configuration;

                services.AddHostedService<ConsoleHostedService>();

                services.AddTransient<IRetriveMessages, MessageRetriever>();

                services.Configure<TelegramAuthenticationConfig>(configurationRoot.GetSection("Telegram:Authentication"));
                services.Configure<TelegramChanneConfig>(configurationRoot.GetSection("Telegram:Channel"));
                services.Configure<StopWordsConfig>(configurationRoot.GetSection("StopWords"));
                services.Configure<OutputFileConfig>(configurationRoot.GetSection("OutputFile"));
            });
            return host;
        }
    }
}
