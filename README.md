# EPR Submission Event Dispatcher

## Overview

This function listens to an Azure ServiceBus Queue, for log messages. It will create a custom event in an event grid containing the data in the Azure ServiceBus Queue.
 
## How To Run 
 
### Prerequisites 
In order to run the service you will need the following dependencies 
 
- .Net 6 
 
### Dependencies 
 
 
 
### Run 
 On root directory, execute
```
make run
```
### Docker
Run in terminal at the solution source root -

```
docker build -t eventdispatcher -f EPR.EventDispatcher.Functions/Dockerfile .
```

Fill out the environment variables and run the following command -
```
docker run -e AzureWebJobsStorage:"X" -e FUNCTIONS_WORKER_RUNTIME:"X" -e FUNCTIONS_WORKER_RUNTIME_VERSION:"X" -e ServiceBus:ConnectionString="X" -e ServiceBus:QueueName="X" eventdispatcher
```

## How To Test 
 
### Unit tests 

On root directory, execute
```
make unit-tests
```
 
 
### Pact tests 
 
N/A
 
### Integration tests

N/A
 
## How To Debug 
 
 
## Environment Variables - deployed environments 
A copy of the configuration file and a description of each variable can be found [here](https://eaflood.atlassian.net/wiki/spaces/MWR/pages/4360830992/Event+Dispatcher+Variables).

## Additional Information 
 
### Logging into Azure 
 
### Usage 
 
### Monitoring and Health Check 
 
## Directory Structure 

### Source files 
- `EPR.EventDispatcher.Application` - Application .Net source files
- `EPR.EventDispatcher.Application.Tests` - .Net unit test files
- `EPR.EventDispatcher.Functions` - Function .Net source files
- `EPR.EventDispatcher.Functions.Tests` - .Net unit test files
 
### Source packages 

## Contributing 
 
