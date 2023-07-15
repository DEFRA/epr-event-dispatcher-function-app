namespace EPR.EventDispatcher.Functions;

using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Application.Extensions;
using Application.Services.Interfaces;

public class ServiceBusQueueTrigger
{
    private readonly IEventService _eventService;
    private readonly ILogger<ServiceBusQueueTrigger> _logger;

    public ServiceBusQueueTrigger(IEventService eventService, ILogger<ServiceBusQueueTrigger> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [FunctionName("ServiceBusQueueTrigger")]
    public async Task RunAsync(
        [ServiceBusTrigger("%ServiceBus:QueueName%", Connection = "ServiceBus:ConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogEnter();

        await _eventService.SendEventAsync(message.Body.ToString());
        await messageActions.CompleteMessageAsync(message);

        _logger.LogExit();
    }
}