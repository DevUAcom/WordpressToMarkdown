using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WordpressToMarkdown;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.dev.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        // Bind settings and register
        var appSettings = new AppSettings();
        context.Configuration.GetSection("AppSettings").Bind(appSettings);
        services.AddSingleton(appSettings);

        // Register services
        services.UseApp();
    })
    .Build();

var app = host.Services.GetRequiredService<App>();
await app.Run();

Console.WriteLine("Done!");

