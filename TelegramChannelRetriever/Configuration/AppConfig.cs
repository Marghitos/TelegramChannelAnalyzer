namespace TelegramChannelAnalyzer.Configuration
{
    public class AppConfig
    {
        public TelegramAuthenticationConfig TelegramAuthenticationConfig { get; set; }
        public TelegramChanneConfig TelegramChannelConfig { get; set; }
        public StopWordsConfig StopWordsConfig { get; set; }
        public OutputFileConfig OutputFileConfig { get; set; }
    }

    public class TelegramAuthenticationConfig
    {
        public string ApiId { get; set; }
        public string ApiHash { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class TelegramChanneConfig
    {
        public long Id { get; set; }
    }

    public class StopWordsConfig
    {
        public string CultureName { get; set; }
    }

    public class OutputFileConfig
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
