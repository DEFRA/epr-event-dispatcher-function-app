namespace EPR.EventDispatcher.Functions.Extensions;

using System.Diagnostics.CodeAnalysis;
using Config;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceExtensions
{
    public static IServiceCollection ConfigureOptions(this IServiceCollection services)
    {
        services.ConfigureSection<EventDispatcherOptions>(EventDispatcherOptions.Section);
        return services;
    }
}