namespace EPR.EventDispatcher.Functions.Tests;

using Application.Services.Interfaces;
using Azure.Messaging.ServiceBus;
using FluentAssertions;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class ServiceBusQueueTriggerTests
{
    private Mock<IEventService> _eventServiceMock;
    private Mock<ILogger<ServiceBusQueueTrigger>> _loggerMock;
    private Mock<ServiceBusMessageActions> _serviceBusMessageActionsMock;

    private ServiceBusQueueTrigger _systemUnderTest;

    [TestInitialize]
    public void TestInitialize()
    {
        _eventServiceMock = new Mock<IEventService>();
        _loggerMock = new Mock<ILogger<ServiceBusQueueTrigger>>();
        _serviceBusMessageActionsMock = new Mock<ServiceBusMessageActions>();
        _systemUnderTest = new ServiceBusQueueTrigger(_eventServiceMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public async Task RunAsync_ProcessesMessage()
    {
        // Arrange
        var serviceBusReceivedMessage = ServiceBusModelFactory.ServiceBusReceivedMessage(
            body: BinaryData.FromString("exampleMessage"),
            properties: null,
            deliveryCount: 1);

        // Act
        await _systemUnderTest.RunAsync(serviceBusReceivedMessage, _serviceBusMessageActionsMock.Object);

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("Entering RunAsync"), Times.Once);
        _eventServiceMock.Verify(x => x.SendEventAsync(serviceBusReceivedMessage.Body.ToString()), Times.Once);
        _serviceBusMessageActionsMock.Verify(x => x.CompleteMessageAsync(serviceBusReceivedMessage, CancellationToken.None), Times.Once);
        _loggerMock.VerifyLog(x => x.LogInformation("Exiting RunAsync"), Times.Once);
    }
}