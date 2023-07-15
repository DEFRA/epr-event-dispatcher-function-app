using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.EventHubs.Producer;
using EPR.EventDispatcher.Application.Services;
using EPR.EventDispatcher.Application.Services.Interfaces;
using EPR.EventDispatcher.Functions;
using EPR.EventDispatcher.Functions.Config;
using EPR.EventDispatcher.Functions.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Startup))]

namespace EPR.EventDispatcher.Functions;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var services = builder.Services
            .AddLogging()
            .AddApplicationInsightsTelemetry()
            .ConfigureOptions();

        if (IsDevelopment())
        {
            services.AddSingleton<IEventService, EventServiceLocal>();
        }
        else
        {
            var serviceProvider = services.BuildServiceProvider();
            var eventDispatcherOptions = serviceProvider.GetRequiredService<IOptions<EventDispatcherOptions>>().Value;

            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton(
                new EventHubProducerClient(
                    eventDispatcherOptions.ConnectionString,
                    eventDispatcherOptions.EventHubName));
        }
    }

    private static bool IsDevelopment()
    {
        return Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development";
    }
}