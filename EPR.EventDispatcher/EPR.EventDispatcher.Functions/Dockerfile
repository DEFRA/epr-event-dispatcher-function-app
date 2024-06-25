# Use the SDK image to build your application
FROM defradigital/dotnetcore-development:dotnet8.0 AS installer-env

# Copy your application source code
COPY EPR.EventDispatcher.Functions/. ./EPR.EventDispatcher.Functions/.
COPY EPR.EventDispatcher.Application/. ./EPR.EventDispatcher.Application/.
COPY stylecop.ruleset ./
COPY Directory.Build.props ./

# Build the application
RUN dotnet publish EPR.EventDispatcher.Functions/*.csproj --output /home/site/wwwroot

# Use the Azure Functions image as the final image
FROM defradigital/dotnetcore:dotnet8.0

# Create a non-root user and set permissions
RUN groupadd -r dotnet && \
    useradd -r -g dotnet dotnet && \
    chown -R dotnet /home

# Switch to the non-root user for the final image
USER dotnet

# Set environment variables
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

# Copy the published application from the installer-env image
COPY --from=installer-env --chown=dotnet ["/home/site/wwwroot", "/home/site/wwwroot"]