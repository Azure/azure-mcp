// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Models.Command;
using AzureMcp.Sql.Commands.FirewallRule;
using AzureMcp.Sql.Models;
using AzureMcp.Sql.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Sql.UnitTests.FirewallRule;

public class FirewallRuleCreateCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISqlService _service;
    private readonly ILogger<FirewallRuleCreateCommand> _logger;
    private readonly FirewallRuleCreateCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public FirewallRuleCreateCommandTests()
    {
        _service = Substitute.For<ISqlService>();
        _logger = Substitute.For<ILogger<FirewallRuleCreateCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("create", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.255", true)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.1", true)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 0.0.0.0 --end-ip 255.255.255.255", true)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip invalid --end-ip 192.168.1.255", false)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.1 --end-ip invalid", false)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.255 --end-ip 192.168.1.1", false)]
    [InlineData("--subscription sub --resource-group rg --server server --start-ip 192.168.1.1 --end-ip 192.168.1.255", false)] // missing name
    [InlineData("--subscription sub --resource-group rg --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.255", false)] // missing server
    [InlineData("--subscription sub --server server --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.255", false)] // missing resource group
    [InlineData("--resource-group rg --server server --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.255", false)] // missing subscription
    public async Task ExecuteAsync_ValidatesInput(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var expectedRule = new SqlServerFirewallRule(
                Name: "rule1",
                Id: "/subscriptions/sub/resourceGroups/rg/providers/Microsoft.Sql/servers/server/firewallRules/rule1",
                Type: "Microsoft.Sql/servers/firewallRules",
                StartIpAddress: "192.168.1.1",
                EndIpAddress: "192.168.1.255"
            );

            _service.CreateFirewallRuleAsync(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Core.Options.RetryPolicyOptions?>(),
                Arg.Any<CancellationToken>())
                .Returns(expectedRule);
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
            Assert.NotEmpty(response.Message);
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceError()
    {
        // Arrange
        _service.CreateFirewallRuleAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Core.Options.RetryPolicyOptions?>(),
            Arg.Any<CancellationToken>())
            .Returns(Task.FromException<SqlServerFirewallRule>(new Exception("Service error")));

        var parseResult = _parser.Parse("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.255");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Service error", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesRequestFailedException_NotFound()
    {
        // Arrange
        var requestEx = new Azure.RequestFailedException(404, "Not found", "ResourceNotFound", null);
        _service.CreateFirewallRuleAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Core.Options.RetryPolicyOptions?>(),
            Arg.Any<CancellationToken>())
            .Returns(Task.FromException<SqlServerFirewallRule>(requestEx));

        var parseResult = _parser.Parse("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.255");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(404, response.Status);
        Assert.Contains("SQL server not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesRequestFailedException_Conflict()
    {
        // Arrange
        var requestEx = new Azure.RequestFailedException(409, "Conflict", "ResourceExists", null);
        _service.CreateFirewallRuleAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Core.Options.RetryPolicyOptions?>(),
            Arg.Any<CancellationToken>())
            .Returns(Task.FromException<SqlServerFirewallRule>(requestEx));

        var parseResult = _parser.Parse("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.255");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(409, response.Status);
        Assert.Contains("firewall rule with this name already exists", response.Message);
    }

    [Theory]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.1 --end-ip 192.168.1.255", true)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 10.0.0.1 --end-ip 10.0.0.1", true)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 0.0.0.0 --end-ip 255.255.255.255", true)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 192.168.1.255 --end-ip 192.168.1.1", false)]
    [InlineData("--subscription sub --resource-group rg --server server --name rule1 --start-ip 10.0.1.1 --end-ip 10.0.0.255", false)]
    public async Task ExecuteAsync_ValidatesIpRange(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var expectedRule = new SqlServerFirewallRule(
                Name: "rule1",
                Id: "/subscriptions/sub/resourceGroups/rg/providers/Microsoft.Sql/servers/server/firewallRules/rule1",
                Type: "Microsoft.Sql/servers/firewallRules",
                StartIpAddress: args.Contains("192.168.1.1") ? "192.168.1.1" : (args.Contains("10.0.0.1") ? "10.0.0.1" : "0.0.0.0"),
                EndIpAddress: args.Contains("192.168.1.255") ? "192.168.1.255" : (args.Contains("10.0.0.1") ? "10.0.0.1" : "255.255.255.255")
            );

            _service.CreateFirewallRuleAsync(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Core.Options.RetryPolicyOptions?>(),
                Arg.Any<CancellationToken>())
                .Returns(expectedRule);
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
            Assert.Contains("Start IP address must be less than or equal to end IP address", response.Message);
        }
    }
}
