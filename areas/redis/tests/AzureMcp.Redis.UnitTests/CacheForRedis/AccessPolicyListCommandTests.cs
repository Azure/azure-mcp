// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Models;
using AzureMcp.Core.Models.Command;
using AzureMcp.Redis.Commands.CacheForRedis;
using AzureMcp.Redis.Models.CacheForRedis;
using AzureMcp.Redis.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Redis.UnitTests.CacheForRedis;

public class AccessPolicyListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRedisService _redisService;
    private readonly ILogger<AccessPolicyListCommand> _logger;
    private readonly AccessPolicyListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";
    private readonly string _knownResourceGroup = "rg1";
    private readonly string _knownCache = "cache1";

    public AccessPolicyListCommandTests()
    {
        _redisService = Substitute.For<IRedisService>();
        _logger = Substitute.For<ILogger<AccessPolicyListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_redisService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsAccessPolicyAssignments_WhenAssignmentsExist()
    {
        var expectedAssignments = new AccessPolicyAssignment[]
        {
            new() { AccessPolicyName = "policy1", IdentityName = "identity1", ProvisioningState = "Succeeded" },
            new() { AccessPolicyName = "policy2", IdentityName = "identity2", ProvisioningState = "Succeeded" }
        };

        _redisService.ListAccessPolicyAssignmentsAsync(
            Arg.Is(_knownCache),
            Arg.Is(_knownResourceGroup),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<AuthMethod>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions>())
            .Returns(expectedAssignments);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--cache", _knownCache
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<AccessPolicyListCommandResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Equal(expectedAssignments.Length, result.AccessPolicyAssignments.Count());
        Assert.Collection(result.AccessPolicyAssignments,
            item => Assert.Equal("policy1", item.AccessPolicyName),
            item => Assert.Equal("policy2", item.AccessPolicyName));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoAccessPolicyAssignments()
    {
        _redisService.ListAccessPolicyAssignmentsAsync(
            Arg.Is(_knownCache),
            Arg.Is(_knownResourceGroup),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<AuthMethod>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions>())
            .Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--cache", _knownCache
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<AccessPolicyListCommandResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.AccessPolicyAssignments);
        Assert.Empty(result.AccessPolicyAssignments);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        _redisService.ListAccessPolicyAssignmentsAsync(
            cacheName: _knownCache,
            resourceGroupName: _knownResourceGroup,
            subscriptionId: _knownSubscriptionId,
            tenant: Arg.Any<string>(),
            authMethod: Arg.Any<AuthMethod>(),
            retryPolicy: Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions>())
            .ThrowsAsync(new Exception("Test error"));

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--cache", _knownCache
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--resource-group")]
    [InlineData("--cache")]
    public async Task ExecuteAsync_ReturnsError_WhenRequiredParameterIsMissing(string parameterToKeep)
    {
        var options = new List<string>();
        if (parameterToKeep == "--subscription")
            options.AddRange(["--subscription", _knownSubscriptionId]);
        if (parameterToKeep == "--resource-group")
            options.AddRange(["--resource-group", _knownResourceGroup]);
        if (parameterToKeep == "--cache")
            options.AddRange(["--cache", _knownCache]);

        var parseResult = _parser.Parse(options.ToArray());

        var response = await _command.ExecuteAsync(_context, parseResult);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message, StringComparison.OrdinalIgnoreCase);
    }

    private record AccessPolicyListCommandResult(IEnumerable<AccessPolicyAssignment> AccessPolicyAssignments)
    {
        public IEnumerable<AccessPolicyAssignment> AccessPolicyAssignments { get; set; } = AccessPolicyAssignments ?? [];
    }
}
