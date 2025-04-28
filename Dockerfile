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

# Set the entry point and default command for the container
ENTRYPOINT ["dotnet", "azmcp.dll"]
CMD ["server", "start", "--transport", "sse"]
