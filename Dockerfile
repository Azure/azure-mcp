# Use the official .NET SDK image as the build image
FROM mcr.microsoft.com/dotnet/sdk:9.0.102-bookworm-slim AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["src/AzureMcp.csproj", "src/"]
RUN dotnet restore "src/AzureMcp.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build "src/AzureMcp.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "src/AzureMcp.csproj" -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim AS runtime
WORKDIR /app

# Install Azure CLI and required dependencies
RUN apt-get update && apt-get install -y \
    curl \
    ca-certificates \
    apt-transport-https \
    lsb-release \
    gnupg \
    && curl -sL https://aka.ms/InstallAzureCLIDeb | bash \
    && rm -rf /var/lib/apt/lists/*

# Copy the published application
COPY --from=publish /app/publish .

# Set environment variables with defaults that can be overridden
# the tranport can be set to "stdio" or "sse".
# "stdio" is the default mode and uses standard input/output for communication.
# "sse" is the server-sent events mode and uses HTTP for communication.
ENV AZMCP_TRANSPORT=stdio
# The port is used by the "sse" transport mode only, and defaults to 5008.
ENV AZMCP_PORT=5008

# Create entrypoint script
RUN echo '#!/bin/bash\n\
TRANSPORT_ARG=""\n\
PORT_ARG=""\n\
\n\
# Only add transport parameter if AZMCP_TRANSPORT is set\n\
if [ -n "$AZMCP_TRANSPORT" ]; then\n\
    TRANSPORT_ARG="--transport $AZMCP_TRANSPORT"\n\
fi\n\
\n\
# Only add port parameter if AZMCP_PORT is set\n\
if [ -n "$AZMCP_PORT" ]; then\n\
    PORT_ARG="--port $AZMCP_PORT"\n\
fi\n\
\n\
if [ "$1" = "server" ] && [ "$2" = "start" ]; then\n\
    # If the command is "server start", apply our transport and port arguments\n\
    shift 2\n\
    dotnet azmcp.dll server start $TRANSPORT_ARG $PORT_ARG "$@"\n\
else\n\
    # Otherwise, pass all arguments through directly\n\
    dotnet azmcp.dll "$@"\n\
fi' > /app/docker-entrypoint.sh \
    && chmod +x /app/docker-entrypoint.sh

# Set the entry point and default command for the container
ENTRYPOINT ["/app/docker-entrypoint.sh"]
CMD ["server", "start"]
