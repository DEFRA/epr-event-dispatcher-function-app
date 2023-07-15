namespace EPR.EventDispatcher.Application.Services;

using Interfaces;
using Microsoft.Extensions.Logging;

public class EventServiceLocal : IEventService
{
    private readonly ILogger<EventServiceLocal> _logger;

    public EventServiceLocal(ILogger<EventServiceLocal> logger)
    {
        _logger = logger;
    }

    public async Task SendEventAsync(string eventData)
    {
        _logger.LogInformation("Log received for {EventData}", eventData);
    }
}