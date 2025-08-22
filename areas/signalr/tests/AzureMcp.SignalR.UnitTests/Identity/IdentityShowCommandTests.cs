// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.SignalR.Commands.Identity;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.CommandLine.Parsing;
using AzureMcp.Core.Models;
using Xunit;

namespace AzureMcp.SignalR.UnitTests.Identity;

public class IdentityShowCommandTests
{
    private readonly ISignalRService _signalRService;
    private readonly ILogger<IdentityShowCommand> _logger;
    private readonly IdentityShowCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public IdentityShowCommandTests()
    {
        _signalRService = Substitute.For<ISignalRService>();
        _logger = Substitute.For<ILogger<IdentityShowCommand>>();

        var collection = new ServiceCollection().AddSingleton(_signalRService);
        var serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("show", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("managed identity configuration", command.Description);
    }

    [Theory]
    [InlineData("--signalr-name testSignalR --resource-group testRG --subscription testSub", true)]
    [InlineData("--signalr-name testSignalR --resource-group testRG", false)] // Missing subscription
    [InlineData("--resource-group testRG --subscription testSub", false)] // Missing signalr-name
    [InlineData("", false)] // Missing all required params
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        var identity = new SignalRIdentityModel
        {
            Type = "SystemAssigned", PrincipalId = "principal123", TenantId = "tenant123"
        };

        _signalRService.GetSignalRIdentityAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(identity);

        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        if (shouldSucceed)
        {
            Assert.Equal(200, response.Status);
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            // Validation failures return 400 status with error message about missing required options
            Assert.Equal(400, response.Status);
            Assert.Contains("Missing Required options", response.Message);
        }
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsIdentitySuccessfully()
    {
        // Arrange
        var expectedIdentity = new SignalRIdentityModel
        {
            Type = "SystemAssigned",
            PrincipalId = "12345678-1234-1234-1234-123456789012",
            TenantId = "87654321-4321-4321-4321-210987654321"
        };

        _signalRService.GetSignalRIdentityAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string?>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedIdentity);

        var parseResult = _parser.Parse([
            "--signalr-name", "testSignalR", "--resource-group", "testRG", "--subscription", "testSub"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Equal("Success", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsUserAssignedIdentitySuccessfully()
    {
        // Arrange
        var expectedIdentity = new SignalRIdentityModel
        {
            Type = "UserAssigned",
            UserAssignedIdentities = new Dictionary<string, UserAssignedIdentity>
            {
                {
                    "/subscriptions/testSub/resourceGroups/testRG/providers/Microsoft.ManagedIdentity/userAssignedIdentities/testIdentity",
                    new UserAssignedIdentity { PrincipalId = "principal123", ClientId = "client123" }
                }
            }
        };

        _signalRService.GetSignalRIdentityAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string?>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedIdentity);

        var parseResult = _parser.Parse([
            "--signalr-name", "testSignalR", "--resource-group", "testRG", "--subscription", "testSub"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Equal("Success", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNotFoundWhenIdentityDoesNotExist()
    {
        // Arrange
        _signalRService.GetSignalRIdentityAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string?>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns((SignalRIdentityModel?)null);

        var parseResult = _parser.Parse([
            "--signalr-name", "testSignalR", "--resource-group", "testRG", "--subscription", "testSub"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(404, response.Status);
        Assert.Contains("does not have managed identity configured", response.Message);
        Assert.Contains("testSignalR", response.Message);
        Assert.Contains("testRG", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        var azureException = new RequestFailedException(500, "Internal server error", "InternalServerError", null);
        _signalRService.GetSignalRIdentityAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string?>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(azureException);

        var parseResult = _parser.Parse([
            "--signalr-name", "testSignalR", "--resource-group", "testRG", "--subscription", "testSub"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Internal server error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesNotFoundExceptionGracefully()
    {
        // Arrange
        var notFoundException = new RequestFailedException(404, "SignalR service not found", "NotFound", null);
        _signalRService.GetSignalRIdentityAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string?>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(notFoundException);

        var parseResult = _parser.Parse([
            "--signalr-name", "nonExistentSignalR", "--resource-group", "testRG", "--subscription", "testSub"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(404, response.Status);
        Assert.Contains("SignalR service not found", response.Message);
    }

    // Service layer tests for backward compatibility
    [Fact]
    public async Task GetSignalRIdentityAsync_ThrowsArgumentNullException_WhenSubscriptionIdIsNull()
    {
        // Arrange
        var service = new SignalRService(
            Substitute.For<ISubscriptionService>(),
            Substitute.For<ITenantService>());

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            service.GetSignalRIdentityAsync(null!, "testRG", "testSignalR"));
    }

    [Fact]
    public async Task GetSignalRIdentityAsync_ThrowsArgumentNullException_WhenResourceGroupIsNull()
    {
        // Arrange
        var service = new SignalRService(
            Substitute.For<ISubscriptionService>(),
            Substitute.For<ITenantService>());

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            service.GetSignalRIdentityAsync("testSub", null!, "testSignalR"));
    }

    [Fact]
    public async Task GetSignalRIdentityAsync_ThrowsArgumentNullException_WhenSignalRNameIsNull()
    {
        // Arrange
        var service = new SignalRService(
            Substitute.For<ISubscriptionService>(),
            Substitute.For<ITenantService>());

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            service.GetSignalRIdentityAsync("testSub", "testRG", null!));
    }
}
