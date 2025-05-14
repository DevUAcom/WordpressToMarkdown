using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WordpressToMarkdown;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, config) =>
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
        services.UseDb(
            context.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException(),
            appSettings.Prefix
        );
    })
    .Build();

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine("Canceling...");
    cts.Cancel();
    e.Cancel = true;
};

var app = host.Services.GetRequiredService<App>();

try
{
    await app.Run(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Canceled!");
}