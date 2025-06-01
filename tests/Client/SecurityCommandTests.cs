// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Commands;
using AzureMcp.Commands.Security.Alert;
using AzureMcp.Services.Azure.Security;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Client;

/// <summary>
/// Integration tests for security-related commands
/// </summary>
public class SecurityCommandTests
{
    private readonly ServiceProvider _serviceProvider;

    public SecurityCommandTests()
    {
        var services = new ServiceCollection();
        
        // Add logging
        services.AddLogging();
        
        // Add mock services for testing
        services.AddSingleton(Substitute.For<ITenantService>());
        services.AddSingleton<ISecurityService, SecurityService>();
        
        // Add command factory
        services.AddSingleton<CommandFactory>();

        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void SecurityCommands_ShouldBeRegistered()
    {
        // Arrange
        var commandFactory = _serviceProvider.GetRequiredService<CommandFactory>();

        // Act
        var securityAlertGetCommand = commandFactory.FindCommandByName("security-alert-get");

        // Assert
        Assert.NotNull(securityAlertGetCommand);
        Assert.IsType<AlertGetCommand>(securityAlertGetCommand);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void SecurityCommands_ShouldHaveCorrectHierarchy()
    {
        // Arrange
        var commandFactory = _serviceProvider.GetRequiredService<CommandFactory>();
        var rootGroup = commandFactory.RootGroup;

        // Act
        var securityGroup = rootGroup.SubGroup.FirstOrDefault(g => g.Name == "security");
        var alertGroup = securityGroup?.SubGroup.FirstOrDefault(g => g.Name == "alert");
        var getCommand = alertGroup?.Commands.ContainsKey("get");

        // Assert
        Assert.NotNull(securityGroup);
        Assert.Equal("security", securityGroup.Name);
        Assert.Contains("Security operations", securityGroup.Description);

        Assert.NotNull(alertGroup);
        Assert.Equal("alert", alertGroup.Name);
        Assert.Contains("Security alert operations", alertGroup.Description);

        Assert.True(getCommand);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public void AlertGetCommand_ShouldHaveCorrectProperties()
    {
        // Arrange
        var commandFactory = _serviceProvider.GetRequiredService<CommandFactory>();

        // Act
        var command = commandFactory.FindCommandByName("security-alert-get") as AlertGetCommand;

        // Assert
        Assert.NotNull(command);
        Assert.Equal("get", command.Name);
        Assert.Equal("Get Security Alert", command.Title);
        Assert.Contains("Get a security alert by ID", command.Description);
        Assert.Contains("Defender for Cloud", command.Description);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void SecurityService_ShouldBeInstantiable()
    {
        // Arrange
        var logger = _serviceProvider.GetRequiredService<ILogger<SecurityService>>();
        var tenantService = _serviceProvider.GetRequiredService<ITenantService>();

        // Act
        var securityService = new SecurityService(tenantService, logger);

        // Assert
        Assert.NotNull(securityService);
        Assert.IsAssignableFrom<ISecurityService>(securityService);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task SecurityService_GetAlertAsync_WithInvalidParameters_ShouldThrowArgumentException()
    {
        // Arrange
        var logger = _serviceProvider.GetRequiredService<ILogger<SecurityService>>();
        var tenantService = _serviceProvider.GetRequiredService<ITenantService>();
        var securityService = new SecurityService(tenantService, logger);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await securityService.GetAlertAsync(null!, "alert-id", cancellationToken: TestContext.Current.CancellationToken));
            
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await securityService.GetAlertAsync("subscription-id", null!, cancellationToken: TestContext.Current.CancellationToken));
            
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await securityService.GetAlertAsync("subscription-id", "", cancellationToken: TestContext.Current.CancellationToken));
    }
}
