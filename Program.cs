using NLog;
using NLog.Config;
using NLog.Targets;
using Redirector;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var config = new LoggingConfiguration();
        var consoleTarget = new ConsoleTarget
        {
            Name = "console",
            Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}",
        };

        config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget, "*");
        LogManager.Configuration = config;

        var logger = LogManager.GetCurrentClassLogger();
        var cacheRefresh = TimeSpan.FromMinutes(2);

        logger.Info("Starting redirection service...");

        var redirectionService = new RedirectionService();
        
        // TODO: Parse json into list

        while (true)
        {
            //var redirectsJson = RedirectProviderService.GetRedirectsJson();
            var redirects = RedirectProviderService.GetRedirectsList();

            logger.Info("Processing requests...");
            await redirectionService.HandleRedirectsAsync(redirects);

            logger.Info("Awaiting next cache refresh...");
            await Task.Delay(cacheRefresh);
            logger.Info("Refreshing cache...");
        }
    }
}