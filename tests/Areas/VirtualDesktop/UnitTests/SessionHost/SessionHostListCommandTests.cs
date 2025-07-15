// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;
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
public class SessionHostListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IVirtualDesktopService _virtualDesktopService;
    private readonly ILogger<SessionHostListCommand> _logger;
    private readonly SessionHostListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public SessionHostListCommandTests()
    {
        _virtualDesktopService = Substitute.For<IVirtualDesktopService>();
        _logger = Substitute.For<ILogger<SessionHostListCommand>>();

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
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Equal("List SessionHosts", _command.Title);
    }

    [Theory]
    [InlineData("--subscription sub123 --hostpool-name pool1", true)]
    [InlineData("--subscription sub123 --hostpool-name pool1 --tenant tenant1", true)]
    [InlineData("--subscription sub123", false)] // Missing hostpool-name
    [InlineData("--hostpool-name pool1", false)] // Missing subscription
    [InlineData("", false)] // Missing both
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _virtualDesktopService.ListSessionHostsAsync(
                Arg.Any<string>(), 
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
                .Returns(new List<string> { "sessionhost1", "sessionhost2" });
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
        var expectedSessionHosts = new List<string> { "sessionhost1", "sessionhost2" };
        _virtualDesktopService.ListSessionHostsAsync(
            "sub123", 
            "pool1",
            null,
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedSessionHosts);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _parser.Parse("--subscription sub123 --hostpool-name pool1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);
        
        await _virtualDesktopService.Received(1).ListSessionHostsAsync(
            "sub123", 
            "pool1", 
            null,
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyResults_ReturnsNullResults()
    {
        // Arrange
        _virtualDesktopService.ListSessionHostsAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(new List<string>());

        var context = new CommandContext(_serviceProvider);
        var parseResult = _parser.Parse("--subscription sub123 --hostpool-name pool1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _virtualDesktopService.ListSessionHostsAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<IReadOnlyList<string>>(new Exception("Test error")));

        var context = new CommandContext(_serviceProvider);
        var parseResult = _parser.Parse("--subscription sub123 --hostpool-name pool1");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--hostpool-name")]  
    [InlineData("--invalid-option")]
    public async Task ExecuteAsync_WithInvalidArgs_ReturnsBadRequest(string invalidArgs)
    {
        // Arrange
        var context = new CommandContext(_serviceProvider);
        
        // Act & Assert
        try
        {
            var parseResult = _parser.Parse(invalidArgs);
            var response = await _command.ExecuteAsync(context, parseResult);
            
            // If parsing succeeds but validation fails, expect 400
            Assert.Equal(400, response.Status);
        }
        catch (InvalidOperationException)
        {
            // This is expected for malformed arguments like incomplete options
            // The parser throws InvalidOperationException for incomplete options
            Assert.True(true, "Expected InvalidOperationException for malformed arguments");
        }
    }
}
