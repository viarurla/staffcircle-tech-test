using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace api;

/// <summary>
/// Entry point of the application
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}