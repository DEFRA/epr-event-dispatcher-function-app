# EPR Event Dispatcher

## Overview

This function listens to an Azure ServiceBus Queue, for log messages. It will create a custom event in an event grid containing the data in the Azure ServiceBus Queue which will then be read by DEFRA SIEM solution.

## Service Bus Payload

```json
{
    "BlobName": "27325295-2c00-49a3-a14b-0200fb959e9d",
    "SubmissionId": "be8fb567-fafb-4892-bc65-ff03ef6732cb",
    "SubmissionSubType": null,
    "OrganisationId": "314be37b-f7f7-44f4-89c8-dea95133b887",
    "UserId": "2171a89a-397e-4806-af06-89f75aac2e3a"
}
```

## How To Run

### Dependencies

In order to run the service you will need the following dependencies:

- .NET 8
- Azure CLI
  
### Run

Go to ```EPR.EventDispatcher\EPR.EventDispatcher.Functions``` directory and execute:

```
func start
```

### Docker

Run in terminal at the solution source root:

```
docker build -t eventdispatcher -f EPR.EventDispatcher.Functions/Dockerfile .
```

Fill out the environment variables and run the following command:

```
docker run -e AzureWebJobsStorage:"X" -e FUNCTIONS_WORKER_RUNTIME:"X" -e FUNCTIONS_WORKER_RUNTIME_VERSION:"X" -e ServiceBus:ConnectionString="X" -e ServiceBus:QueueName="X" eventdispatcher
```

## How To Test

### Unit tests

Go to ```EPR.EventDispatcher``` and execute:

```
dotnet test
```

### Pact tests

N/A

### Integration tests

N/A

## How To Debug

Use debugging tools in your chosen IDE

## Environment Variables - deployed environments

The structure of the appsettings can be found in the repository. Example configurations for the different environments can be found in [epr-app-config-settings](https://dev.azure.com/defragovuk/RWD-CPR-EPR4P-ADO/_git/epr-app-config-settings).

| Variable Name                            | Description                                          |
|------------------------------------------|------------------------------------------------------|
| IsEncrypted                              | Is event data encrypted                              |
| AzureWebJobsStorage                      | The connection string for the Azure Web Job Storage  |
| FUNCTIONS_WORKER_RUNTIME                 | Function runtime                                     |
| ServiceBus__ConnectionString             | The connection string for the service bus            |
| ServiceBus__QueueName                    | The name of the upload queue                         |
| EventDispatcherOptions__ConnectionString | The connection string for Event Hub                  |
| EventDispatcherOptions__EventHubName     | The Event Hub name                                   |

## Additional Information

[ADR-028: Protective Monitoring](https://eaflood.atlassian.net/wiki/spaces/MWR/pages/4334060015/ADR-028+Protective+Monitoring+Logging)

### Monitoring and Health Check

Enable Health Check in the Azure portal and set the URL path to ServiceBusQueueTrigger

## Directory Structure

### Source files

- `EPR.EventDispatcher.Application` - Application .NET source files
- `EPR.EventDispatcher.Application.UnitTests` - .NET unit test files
- `EPR.EventDispatcher.Functions` - Function .NET source files
- `EPR.EventDispatcher.Functions.UnitTests` - .NET unit test files

## Contributing to this project

Please read the [contribution guidelines](CONTRIBUTING.md) before submitting a pull request.

## Licence

[Licence information](LICENCE.md).