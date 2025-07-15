// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.ContainerApps.Commands.ContainerRegistry;
using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Areas.ContainerApps.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.ContainerApps.UnitTests.ContainerRegistry;

[Trait("Area", "ContainerApps")]
public partial class ContainerRegistryCreateCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContainerAppsService _containerAppsService;
    private readonly ILogger<ContainerRegistryCreateCommand> _logger;
    private readonly ContainerRegistryCreateCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public ContainerRegistryCreateCommandTests()
    {
        _containerAppsService = Substitute.For<IContainerAppsService>();
        _logger = Substitute.For<ILogger<ContainerRegistryCreateCommand>>();

        var collection = new ServiceCollection().AddSingleton(_containerAppsService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_CreatesContainerRegistry()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg123";
        var registryName = "myregistry123";
        var location = "eastus";
        var sku = "Basic";

        var expectedRegistry = new AzureMcp.Areas.ContainerApps.Models.ContainerRegistry
        {
            Name = registryName,
            ResourceGroup = resourceGroup,
            Location = location,
            LoginServer = $"{registryName}.azurecr.io",
            CreationDate = DateTime.UtcNow,
            Sku = sku,
            AdminUserEnabled = false,
            Tags = new Dictionary<string, string>(),
            ProvisioningState = "Succeeded",
            PublicNetworkAccess = "Enabled"
        };

        _containerAppsService.CreateContainerRegistry(
            Arg.Is(registryName),
            Arg.Is(resourceGroup),
            Arg.Is(subscriptionId),
            Arg.Is(location),
            Arg.Is(sku),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedRegistry);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--resource-group", resourceGroup,
            "--registry-name", registryName,
            "--location", location,
            "--sku-name", sku
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ContainerRegistryCreateResult>(json);

        Assert.NotNull(result);
        Assert.Equal(registryName, result.Registry.Name);
        Assert.Equal(resourceGroup, result.Registry.ResourceGroup);
        Assert.Equal(location, result.Registry.Location);
        Assert.Equal(sku, result.Registry.Sku);
    }

    [Fact]
    public async Task ExecuteAsync_MissingRequiredParameters_ReturnsValidationError()
    {
        // Arrange
        var args = _parser.Parse(["--subscription", "sub123"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_HandlesGracefully()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg123";
        var registryName = "myregistry123";
        var location = "eastus";

        _containerAppsService.CreateContainerRegistry(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception("Service error"));

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--resource-group", resourceGroup,
            "--registry-name", registryName,
            "--location", location
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
        Assert.Contains("Service error", response.Message);
    }

    [JsonSerializable(typeof(AzureMcp.Areas.ContainerApps.Models.ContainerRegistry))]
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    internal sealed partial class TestJsonContext : JsonSerializerContext
    {
    }

    internal record ContainerRegistryCreateResult(AzureMcp.Areas.ContainerApps.Models.ContainerRegistry Registry);
}
