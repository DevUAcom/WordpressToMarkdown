using Microsoft.Extensions.DependencyInjection;

namespace WordpressToMarkdown;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseApp(this IServiceCollection services) => 
        services.AddSingleton<App>();
}