using Redirector;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Starting redirection service...");

        var redirectionService = new RedirectionService();
        
        // TODO: Parse json into list
        var redirectsJson = RedirectProviderService.GetRedirectsJson();
        var redirects = RedirectProviderService.GetRedirectsList();

        foreach (var redirect in redirects)
        {
            redirectionService.HandleRedirect(redirect);
        }

        System.Console.WriteLine("All redirects processed...");
        System.Console.WriteLine("Exiting...");
    }
}