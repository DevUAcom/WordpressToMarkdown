using WordpressToMarkdown;
using WordpressToMarkdown.DataProviders;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseApp(this IServiceCollection services) => 
        services.AddSingleton<App>();
    
    public static IServiceCollection UseDb(this IServiceCollection services, string connectionString, string prefix) =>
        services.AddSingleton<IDataProvider, DbDataProvider>(_ => new DbDataProvider(connectionString, prefix));
}