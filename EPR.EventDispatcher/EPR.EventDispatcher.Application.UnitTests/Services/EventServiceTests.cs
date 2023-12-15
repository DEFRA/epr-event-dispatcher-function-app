namespace EPR.EventDispatcher.Application.UnitTests.Services;

using Application.Services;
using Application.Services.Interfaces;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class EventServiceTests
{
    private Mock<EventHubProducerClient> _clientMock;
    private Mock<ILogger<EventService>> _loggerMock;
    private IEventService _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _clientMock = new Mock<EventHubProducerClient>();
        _loggerMock = new Mock<ILogger<EventService>>();
        _systemUnderTest = new EventService(_clientMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public async Task AddEvent_LogsAndSendsEvent_WhenNoExceptionsAreThrown()
    {
        // Arrange
        const int eventLimit = 3;
        const string eventData = "Test Event Data";
        var store = new List<EventData>();
        var eventDataBatch = EventHubsModelFactory.EventDataBatch(5, store, tryAddCallback: _ => store.Count < eventLimit);
        _clientMock.Setup(x => x.CreateBatchAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(eventDataBatch);

        // Act & Assert
        await _systemUnderTest
            .Invoking(x => x.SendEventAsync(eventData))
            .Should()
            .NotThrowAsync();
        _loggerMock.VerifyLog(x => x.LogInformation("Event posted to Event Hub"));
    }

    [TestMethod]
    public async Task AddEvent_LogsAndThrowsException_SendEventAsyncThrowsException()
    {
        // Arrange
        _clientMock
            .Setup(x => x.SendAsync(
                It.IsAny<EventDataBatch>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());

        const string eventData = "Test Event Data";

        // Act & Assert
        await _systemUnderTest
            .Invoking(x => x.SendEventAsync(eventData))
            .Should()
            .ThrowAsync<Exception>();
        _loggerMock.VerifyLog(x => x.LogError("Failed to post event to Event Hub"));
    }
}