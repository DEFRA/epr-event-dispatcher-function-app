namespace EPR.EventDispatcher.Application.Tests.Services;

using Application.Services;
using Application.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class EventServiceLocalTests
{
    private Mock<ILogger<EventServiceLocal>> _loggerMock;
    private IEventService _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _loggerMock = new Mock<ILogger<EventServiceLocal>>();
        _systemUnderTest = new EventServiceLocal(_loggerMock.Object);
    }

    [TestMethod]
    public async Task AddEvent_LogsEvent()
    {
        // Arrange
        const string eventData = "event string";

        // Act & Assert
        await _systemUnderTest
            .Invoking(x => x.SendEventAsync(eventData))
            .Should()
            .NotThrowAsync();
        _loggerMock.VerifyLog(x => x.LogInformation("Log received for {EventData}", eventData), Times.Once);
    }
}