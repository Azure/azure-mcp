// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;
using AzureMcp.Areas.VirtualDesktop.Models;
using AzureMcp.Areas.VirtualDesktop.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.VirtualDesktop.UnitTests.SessionHost;

[Trait("Area", "VirtualDesktop")]
public class SessionHostUserSessionListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IVirtualDesktopService _virtualDesktopService;
    private readonly ILogger<SessionHostUserSessionListCommand> _logger;
    private readonly SessionHostUserSessionListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public SessionHostUserSessionListCommandTests()
    {
        _virtualDesktopService = Substitute.For<IVirtualDesktopService>();
        _logger = Substitute.For<ILogger<SessionHostUserSessionListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_virtualDesktopService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("usersession-list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Equal("List User Sessions on Session Host", _command.Title);
    }

    [Theory]
    [InlineData("--subscription sub123 --hostpool-name pool1 --sessionhost-name host1", true)]
    [InlineData("--subscription sub123 --hostpool-name pool1 --sessionhost-name host1 --tenant tenant1", true)]
    [InlineData("--subscription sub123 --hostpool-name pool1", false)] // Missing sessionhost-name
    [InlineData("--subscription sub123 --sessionhost-name host1", false)] // Missing hostpool-name
    [InlineData("--hostpool-name pool1 --sessionhost-name host1", false)] // Missing subscription
    [InlineData("", false)] // Missing all required
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var mockUserSessions = new List<UserSession>
            {
                CreateMockUserSession("user1"),
                CreateMockUserSession("user2")
            };
            
            _virtualDesktopService.ListUserSessionsAsync(
                Arg.Any<string>(), 
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
                .Returns(Task.FromResult<IReadOnlyList<UserSession>>(mockUserSessions));
        }

        var context = new CommandContext(_serviceProvider);
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_WithValidInput_CallsServiceCorrectly()
    {
        // Arrange
        var expectedUserSessions = new List<UserSession>
        {
            new() { Name = "session1", UserPrincipalName = "user1@contoso.com", SessionState = "Active" },
            new() { Name = "session2", UserPrincipalName = "user2@contoso.com", SessionState = "Disconnected" }
        };
        _virtualDesktopService.ListUserSessionsAsync(
            "sub123", 
            "pool1",
            "host1",
            null,
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedUserSessions);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _parser.Parse("--subscription sub123 --hostpool-name pool1 --sessionhost-name host1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        await _virtualDesktopService.Received(1).ListUserSessionsAsync(
            "sub123",
            "pool1", 
            "host1",
            null,
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyResults_ReturnsSuccessWithNoResults()
    {
        // Arrange
        var emptyUserSessions = new List<UserSession>();
        _virtualDesktopService.ListUserSessionsAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(emptyUserSessions);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _parser.Parse("--subscription sub123 --hostpool-name pool1 --sessionhost-name host1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_WithServiceException_ReturnsError()
    {
        // Arrange
        var exception = new Exception("Service error");
        _virtualDesktopService.ListUserSessionsAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(exception);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _parser.Parse("--subscription sub123 --hostpool-name pool1 --sessionhost-name host1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("error", response.Message.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_WithTenantParameter_PassesTenantToService()
    {
        // Arrange
        var userSessions = new List<UserSession> { CreateMockUserSession("user1") };
        _virtualDesktopService.ListUserSessionsAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(userSessions);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _parser.Parse("--subscription sub123 --hostpool-name pool1 --sessionhost-name host1 --tenant tenant1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        await _virtualDesktopService.Received(1).ListUserSessionsAsync(
            "sub123",
            "pool1",
            "host1",
            "tenant1",
            Arg.Any<RetryPolicyOptions>());
    }

    private static UserSession CreateMockUserSession(string userPrincipalName)
    {
        return new UserSession
        {
            Name = $"session-{userPrincipalName}",
            UserPrincipalName = $"{userPrincipalName}@contoso.com",
            SessionState = "Active",
            ApplicationType = "Desktop",
            ActiveDirectoryUserName = userPrincipalName,
            CreateTime = DateTimeOffset.UtcNow,
            HostPoolName = "pool1",
            SessionHostName = "host1"
        };
    }
}
