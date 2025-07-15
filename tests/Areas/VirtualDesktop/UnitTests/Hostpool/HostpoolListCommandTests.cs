// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.VirtualDesktop.Commands.Hostpool;
using AzureMcp.Areas.VirtualDesktop.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.VirtualDesktop.UnitTests.Hostpool;

[Trait("Area", "VirtualDesktop")]
public class HostpoolListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IVirtualDesktopService _virtualDesktopService;
    private readonly ILogger<HostpoolListCommand> _logger;
    private readonly HostpoolListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public HostpoolListCommandTests()
    {
        _virtualDesktopService = Substitute.For<IVirtualDesktopService>();
        _logger = Substitute.For<ILogger<HostpoolListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_virtualDesktopService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        // Act
        var command = _command.GetCommand();

        // Assert
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Equal("List hostpools", _command.Title);
    }

    [Theory]
    [InlineData("--subscription test-sub", true)]
    [InlineData("--subscription test-sub --tenant test-tenant", true)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _virtualDesktopService.ListHostpoolsAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
                .Returns(new List<string> { "hostpool1", "hostpool2" }.AsReadOnly());
        }

        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        if (shouldSucceed)
        {
            Assert.Equal(200, response.Status);
            Assert.NotNull(response.Results);
        }
        else
        {
            Assert.Equal(400, response.Status);
            Assert.Contains("required", response.Message?.ToLower() ?? "");
        }
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyResult_WhenNoHostpools()
    {
        // Arrange
        _virtualDesktopService.ListHostpoolsAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(new List<string>().AsReadOnly());

        var parseResult = _parser.Parse("--subscription test-sub");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _virtualDesktopService.ListHostpoolsAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<IReadOnlyList<string>>(new Exception("Test error")));

        var parseResult = _parser.Parse("--subscription test-sub");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsHostpools_WhenSuccessful()
    {
        // Arrange
        var expectedHostpools = new List<string> { "hostpool1", "hostpool2" }.AsReadOnly();
        _virtualDesktopService.ListHostpoolsAsync("test-sub", null, Arg.Any<RetryPolicyOptions>())
            .Returns(expectedHostpools);

        var parseResult = _parser.Parse("--subscription test-sub");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        
        await _virtualDesktopService.Received(1).ListHostpoolsAsync("test-sub", null, Arg.Any<RetryPolicyOptions>());
    }
}