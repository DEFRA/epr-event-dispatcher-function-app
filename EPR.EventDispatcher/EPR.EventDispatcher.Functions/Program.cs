#pragma warning disable SA1200
using Azure.Messaging.EventHubs.Producer;
using EPR.EventDispatcher.Application.Services;
using EPR.EventDispatcher.Application.Services.Interfaces;
using EPR.EventDispatcher.Functions.Config;
using EPR.EventDispatcher.Functions.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
#pragma warning restore SA1200

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