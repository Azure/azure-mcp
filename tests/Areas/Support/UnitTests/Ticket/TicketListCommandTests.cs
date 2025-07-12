// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.Support.Commands.Ticket;
using AzureMcp.Areas.Support.Models;
using AzureMcp.Areas.Support.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using AzureMcp.Services.Azure.Tenant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Support.UnitTests.Ticket;

[Trait("Area", "Support")]
public class TicketListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISupportService _supportService;
    private readonly ISupportFilterProcessor _filterProcessor;
    private readonly ITenantService _tenantService;
    private readonly ILogger<TicketListCommand> _logger;
    private readonly TicketListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public TicketListCommandTests()
    {
        _supportService = Substitute.For<ISupportService>();
        _filterProcessor = Substitute.For<ISupportFilterProcessor>();
        _tenantService = Substitute.For<ITenantService>();
        _logger = Substitute.For<ILogger<TicketListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_supportService);
        collection.AddSingleton(_filterProcessor);
        collection.AddSingleton(_tenantService);
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
        Assert.Contains("support tickets", command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidParameters_ReturnsTickets()
    {
        // Arrange
        var expectedTickets = new List<SupportTicket>
        {
            new()
            {
                Id = "/subscriptions/test-sub/providers/Microsoft.Support/supportTickets/test-ticket-1",
                Name = "test-ticket-1",
                Title = "Test Ticket 1",
                Status = "Open",
                Severity = "Moderate"
            }
        };

        _supportService.ListSupportTickets("test-subscription", null, 100, null, Arg.Any<RetryPolicyOptions>())
            .Returns(expectedTickets);

        var args = _parser.Parse(["--subscription", "test-subscription"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_WithFilter_PassesFilterToService()
    {
        // Arrange
        _supportService.ListSupportTickets("test-subscription", "Open", 100, null, Arg.Any<RetryPolicyOptions>())
            .Returns(new List<SupportTicket>());

        var args = _parser.Parse(["--subscription", "test-subscription", "--filter", "Open"]);

        // Act
        await _command.ExecuteAsync(_context, args);

        // Assert
        await _supportService.Received(1).ListSupportTickets("test-subscription", "Open", 100, null, Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithTop_PassesTopToService()
    {
        // Arrange
        _supportService.ListSupportTickets("test-subscription", null, 50, null, Arg.Any<RetryPolicyOptions>())
            .Returns(new List<SupportTicket>());

        var args = _parser.Parse(["--subscription", "test-subscription", "--top", "50"]);

        // Act
        await _command.ExecuteAsync(_context, args);

        // Assert
        await _supportService.Received(1).ListSupportTickets("test-subscription", null, 50, null, Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_HandlesGracefully()
    {
        // Arrange
        _supportService.ListSupportTickets(Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<int?>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new UnauthorizedAccessException("Access denied"));

        var args = _parser.Parse(["--subscription", "test-subscription"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(403, response.Status);
        Assert.Contains("permission", response.Message.ToLowerInvariant());
    }

    [Theory]
    [InlineData("")]
    public async Task ExecuteAsync_MissingRequiredParameters_ReturnsError(string args)
    {
        // Arrange
        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.True(response.Status >= 400);
    }

    [Fact]
    public async Task ExecuteAsync_IncompleteSubscriptionParameter_ThrowsException()
    {
        // Arrange
        var parseResult = _parser.Parse(["--subscription"]);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _command.ExecuteAsync(_context, parseResult));
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceError()
    {
        // Arrange
        _supportService.ListSupportTickets(
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<int?>(),
                Arg.Any<string?>(),
                Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new Exception("Test error"));

        var args = _parser.Parse(["--subscription", "test-subscription"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }
}
