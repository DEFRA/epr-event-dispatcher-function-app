#!/usr/bin/env bash

dotnet test EPR.EventDispatcher/EPR.EventDispatcher.Functions.UnitTests/EPR.EventDispatcher.Functions.UnitTests.csproj --logger "trx;logfilename=testResults.Functions.trx"

dotnet test EPR.EventDispatcher/EPR.EventDispatcher.Application.UnitTests/EPR.EventDispatcher.Application.UnitTests.csproj --logger "trx;logfilename=testResults.Application.trx"
