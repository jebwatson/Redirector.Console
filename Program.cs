using Redirector;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var cacheRefresh = TimeSpan.FromMinutes(2);

        Console.WriteLine("Starting redirection service...");

        var redirectionService = new RedirectionService();
        
        // TODO: Parse json into list

        while (true)
        {
            //var redirectsJson = RedirectProviderService.GetRedirectsJson();
            var redirects = RedirectProviderService.GetRedirectsList();

            System.Console.WriteLine("Processing requests...");
            await redirectionService.HandleRedirectsAsync(redirects);

            await Task.Delay(cacheRefresh);
        }
    }
}