namespace EPR.EventDispatcher.Application.Services.Interfaces;

public interface IEventService
{
    Task SendEventAsync(string eventData);
}