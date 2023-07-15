﻿namespace EPR.EventDispatcher.Application.Tests.Extensions;

using Application.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class LoggerExtensionsTests
{
    private Mock<ILogger<LoggerExtensionsTests>> _loggerMock;

    [TestInitialize]
    public void Initialize()
    {
        _loggerMock = new Mock<ILogger<LoggerExtensionsTests>>();
    }

    [TestMethod]
    public async Task LogEnter_LogsEnteringCallingMethodName_WhenCalled()
    {
        // Act
        _loggerMock.Object.LogEnter();

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("Entering LogEnter_LogsEnteringCallingMethodName_WhenCalled"), Times.Once);
    }

    [TestMethod]
    public async Task LogExit_LogsExitingCallingMethodName_WhenCalled()
    {
        // Act
        _loggerMock.Object.LogExit();

        // Assert
        _loggerMock.VerifyLog(x => x.LogInformation("Exiting LogExit_LogsExitingCallingMethodName_WhenCalled"), Times.Once);
    }
}