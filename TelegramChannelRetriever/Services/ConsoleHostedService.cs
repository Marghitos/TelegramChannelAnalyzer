using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramChannelAnalyzer.Services;

namespace TelegramChannelAnalyzer
{
    public class ConsoleHostedService : IHostedService
    {
        private int? _exitCode;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IRetriveMessages _channelRetriever;

        public ConsoleHostedService(
            IServiceScopeFactory scopeFactory,
            IHostApplicationLifetime appLifetime,
            IRetriveMessages channelRetriever
            )
        {
            _scopeFactory = scopeFactory;
            _appLifetime = appLifetime;
            _channelRetriever = channelRetriever;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await _channelRetriever.ParseMessage();
                        _exitCode = 0;
                    }
                    catch (Exception ex)
                    {
                        _exitCode = 1;
                    }
                    finally
                    {
                        _appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }
    }
}
