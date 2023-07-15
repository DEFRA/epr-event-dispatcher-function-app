namespace EPR.EventDispatcher.Application.Services;

using System.Text;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Interfaces;
using Microsoft.Extensions.Logging;

public class EventService : IEventService
{
    private readonly EventHubProducerClient _client;
    private readonly ILogger<EventService> _logger;

    public EventService(EventHubProducerClient client, ILogger<EventService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task SendEventAsync(string eventData)
    {
        try
        {
            using var eventDataBatch = await _client.CreateBatchAsync();
            eventDataBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventData)));

            await _client.SendAsync(eventDataBatch);
            _logger.LogInformation("Event posted to Event Hub");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to post event to Event Hub");
            throw;
        }
    }
}