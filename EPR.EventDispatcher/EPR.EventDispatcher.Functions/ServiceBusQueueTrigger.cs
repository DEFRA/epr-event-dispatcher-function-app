namespace EPR.EventDispatcher.Functions;

using System.Threading.Tasks;
using Application.Extensions;
using Application.Services.Interfaces;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

public class ServiceBusQueueTrigger
{
    private readonly IEventService _eventService;
    private readonly ILogger<ServiceBusQueueTrigger> _logger;

    public ServiceBusQueueTrigger(IEventService eventService, ILogger<ServiceBusQueueTrigger> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [Function(nameof(ServiceBusQueueTrigger))]
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
