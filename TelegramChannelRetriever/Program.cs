using Microsoft.Extensions.Hosting;

namespace TelegramChannelAnalyzer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await Host.CreateDefaultBuilder(args)
                    .AddConfiguration()                  
                    .AddServices()
                    .RunConsoleAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception: {ex.Message}");
            }
        }

    }
}
