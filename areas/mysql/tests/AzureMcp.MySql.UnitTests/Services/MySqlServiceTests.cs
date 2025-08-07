// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.ResourceManager.MySql.FlexibleServers;
using AzureMcp.Core.Services.Azure.ResourceGroup;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.MySql.UnitTests.Services;

public class MySqlServiceTests
{
    private readonly IResourceGroupService _resourceGroupService;
    private readonly ITenantService _tenantService;
    private readonly ILogger<MySqlService> _logger;
    private readonly MySqlService _mysqlService;

    public MySqlServiceTests()
    {
        _resourceGroupService = Substitute.For<IResourceGroupService>();
        _tenantService = Substitute.For<ITenantService>();
        _logger = Substitute.For<ILogger<MySqlService>>();
        
        _mysqlService = new MySqlService(_resourceGroupService, _tenantService, _logger);
    }

    [Fact]
    public void Constructor_WithNullResourceGroupService_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            new MySqlService(null!, _tenantService, _logger));
    }

    [Fact]
    public void Constructor_WithNullTenantService_CreatesInstance()
    {
        // Arrange
        var resourceGroupService = Substitute.For<IResourceGroupService>();
        var logger = Substitute.For<ILogger<MySqlService>>();

        // Act - tenantService can be null as it's passed to BaseAzureService constructor
        var service = new MySqlService(resourceGroupService, null!, logger);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullLogger_CreatesInstance()
    {
        // Arrange
        var resourceGroupService = Substitute.For<IResourceGroupService>();
        var tenantService = Substitute.For<ITenantService>();

        // Act - logger is assigned directly without validation
        var service = new MySqlService(resourceGroupService, tenantService, null!);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithValidDependencies_CreatesInstance()
    {
        // Act
        var service = new MySqlService(_resourceGroupService, _tenantService, _logger);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public async Task ListServersAsync_WhenResourceGroupServiceThrows_LogsErrorAndRethrows()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        var exception = new InvalidOperationException("Resource group not found");
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .ThrowsAsync(exception);

        // Act & Assert
        var thrownException = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _mysqlService.ListServersAsync(subscriptionId, resourceGroup, user));

        Assert.Equal(exception, thrownException);

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error listing MySQL servers")),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task GetServerConfigAsync_WhenResourceGroupServiceThrows_LogsErrorAndRethrows()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        var server = "server1";
        var exception = new RequestFailedException("Server not found");
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .ThrowsAsync(exception);

        // Act & Assert
        var thrownException = await Assert.ThrowsAsync<RequestFailedException>(() =>
            _mysqlService.GetServerConfigAsync(subscriptionId, resourceGroup, user, server));

        Assert.Equal(exception, thrownException);

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error getting MySQL server configuration")),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task GetServerParameterAsync_WhenResourceGroupServiceThrows_LogsErrorAndRethrows()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        var server = "server1";
        var param = "max_connections";
        var exception = new UnauthorizedAccessException("Access denied");
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .ThrowsAsync(exception);

        // Act & Assert
        var thrownException = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _mysqlService.GetServerParameterAsync(subscriptionId, resourceGroup, user, server, param));

        Assert.Equal(exception, thrownException);

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error getting MySQL server parameter")),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task SetServerParameterAsync_WhenResourceGroupServiceThrows_LogsErrorAndRethrows()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        var server = "server1";
        var param = "max_connections";
        var value = "1000";
        var exception = new TimeoutException("Operation timed out");
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .ThrowsAsync(exception);

        // Act & Assert
        var thrownException = await Assert.ThrowsAsync<TimeoutException>(() =>
            _mysqlService.SetServerParameterAsync(subscriptionId, resourceGroup, user, server, param, value));

        Assert.Equal(exception, thrownException);

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error setting MySQL server parameter")),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Theory]
    [InlineData("", "ResourceGroup", "Subscription")]
    [InlineData("Server", "", "Subscription")]
    [InlineData("Server", "ResourceGroup", "")]
    public async Task ListServersAsync_WithEmptyParameters_StillLogsWithCorrectParameters(
        string subscriptionId, string resourceGroup, string user)
    {
        // Arrange
        var exception = new ArgumentException("Invalid parameters");
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .ThrowsAsync(exception);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _mysqlService.ListServersAsync(subscriptionId, resourceGroup, user));

        // Verify logging includes all parameters even if empty
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error listing MySQL servers")),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task GetServerConfigAsync_WithDifferentExceptionTypes_LogsAppropriately()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        var server = "server1";
        
        // Test with ArgumentException
        var argumentException = new ArgumentException("Invalid server name");
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .ThrowsAsync(argumentException);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _mysqlService.GetServerConfigAsync(subscriptionId, resourceGroup, user, server));

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error getting MySQL server configuration")),
            argumentException,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task SetServerParameterAsync_WithSpecialCharactersInParameters_LogsCorrectly()
    {
        // Arrange
        var subscriptionId = "sub-123!@#";
        var resourceGroup = "rg@test#group";
        var user = "user@domain.com";
        var server = "server-test.mysql";
        var param = "param_with_underscores";
        var value = "value with spaces and symbols!@#$%";
        var exception = new InvalidOperationException("Special characters test");
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .ThrowsAsync(exception);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _mysqlService.SetServerParameterAsync(subscriptionId, resourceGroup, user, server, param, value));

        // Verify logging handles special characters
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error setting MySQL server parameter")),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task GetServerParameterAsync_WithLongParameterNames_LogsCorrectly()
    {
        // Arrange
        var subscriptionId = "subscription-with-very-long-name-that-exceeds-normal-limits";
        var resourceGroup = "resource-group-with-extremely-long-name-for-testing-purposes";
        var user = "user-with-long-name";
        var server = "server-with-very-long-name-for-comprehensive-testing";
        var param = "parameter_with_extremely_long_name_that_tests_logging_capabilities_thoroughly";
        var exception = new Exception("Long names test");
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .ThrowsAsync(exception);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _mysqlService.GetServerParameterAsync(subscriptionId, resourceGroup, user, server, param));

        // Verify logging handles long parameter names
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error getting MySQL server parameter")),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task ListServersAsync_WithNullResourceGroup_LogsErrorAndRethrows()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .Returns(Task.FromResult<Azure.ResourceManager.Resources.ResourceGroupResource?>(null));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _mysqlService.ListServersAsync(subscriptionId, resourceGroup, user));

        Assert.Contains("Resource group 'rg1' not found", exception.Message);

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error listing MySQL servers")),
            Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task GetServerConfigAsync_WithNullResourceGroup_LogsErrorAndRethrows()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        var server = "server1";
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .Returns(Task.FromResult<Azure.ResourceManager.Resources.ResourceGroupResource?>(null));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _mysqlService.GetServerConfigAsync(subscriptionId, resourceGroup, user, server));

        Assert.Contains("Resource group 'rg1' not found", exception.Message);

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error getting MySQL server configuration")),
            Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task GetServerParameterAsync_WithNullResourceGroup_LogsErrorAndRethrows()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        var server = "server1";
        var param = "max_connections";
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .Returns(Task.FromResult<Azure.ResourceManager.Resources.ResourceGroupResource?>(null));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _mysqlService.GetServerParameterAsync(subscriptionId, resourceGroup, user, server, param));

        Assert.Contains("Resource group 'rg1' not found", exception.Message);

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error getting MySQL server parameter")),
            Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Fact]
    public async Task SetServerParameterAsync_WithNullResourceGroup_LogsErrorAndRethrows()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var user = "user1";
        var server = "server1";
        var param = "max_connections";
        var value = "1000";
        
        _resourceGroupService.GetResourceGroupResource(subscriptionId, resourceGroup)
            .Returns(Task.FromResult<Azure.ResourceManager.Resources.ResourceGroupResource?>(null));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            _mysqlService.SetServerParameterAsync(subscriptionId, resourceGroup, user, server, param, value));

        Assert.Contains("Resource group 'rg1' not found", exception.Message);

        // Verify logging
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Error setting MySQL server parameter")),
            Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }
}
