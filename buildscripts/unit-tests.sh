#!/usr/bin/env bash

dotnet test EPR.EventDispatcher/EPR.EventDispatcher.Functions.Tests/EPR.EventDispatcher.Functions.Tests.csproj --logger "trx;logfilename=testResults.Functions.trx"

dotnet test EPR.EventDispatcher/EPR.EventDispatcher.Application.Tests/EPR.EventDispatcher.Application.Tests.csproj --logger "trx;logfilename=testResults.Application.trx"
