namespace EPR.EventDispatcher.Functions;

using System.Diagnostics.CodeAnalysis;
using Azure.Messaging.EventHubs.Producer;
using Application.Services;
using Application.Services.Interfaces;
using Config;
using Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static async Task Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices((context, services) =>
            {
                services
                    .AddApplicationInsightsTelemetryWorkerService()
                    .ConfigureFunctionsApplicationInsights()
                    .ConfigureOptions();

                if (context.HostingEnvironment.IsDevelopment())
                {
                    services.AddSingleton<IEventService, EventServiceLocal>();
                }
                else
                {
                    services.AddSingleton<IEventService, EventService>();
                    services.AddSingleton(sp =>
                    {
                        var options = sp.GetRequiredService<IOptions<EventDispatcherOptions>>().Value;
                        return new EventHubProducerClient(options.ConnectionString, options.EventHubName);
                    });
                }
            })
            .Build();

        await host.RunAsync();
    }
}
