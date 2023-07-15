namespace EPR.EventDispatcher.Functions.Config;

public class EventDispatcherOptions
{
    public const string Section = "EventDispatcher";

    public string ConnectionString { get; init; }

    public string EventHubName { get; init; }
}