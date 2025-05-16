// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Commands.Redis.CacheForRedis;
using AzureMcp.Models.Command;
using AzureMcp.Models.Redis.CacheForRedis;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Commands.Redis.CacheForRedis;

public class AccessPolicyListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRedisService _redisService;
    private readonly ILogger<AccessPolicyListCommand> _logger;

    public AccessPolicyListCommandTests()
    {
        _redisService = Substitute.For<IRedisService>();
        _logger = Substitute.For<ILogger<AccessPolicyListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_redisService);

        _serviceProvider = collection.BuildServiceProvider();
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
            "cache1",
            "rg1",
            "sub123",
            Arg.Any<string>(),
            Arg.Any<AzureMcp.Models.AuthMethod>(),
            Arg.Any<AzureMcp.Arguments.RetryPolicyArguments>())
            .Returns(expectedAssignments);

        var command = new AccessPolicyListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--resource-group", "rg1", "--cache", "cache1"]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

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
    public async Task ExecuteAsync_ReturnsNull_WhenNoAccessPolicyAssignments()
    {
        _redisService.ListAccessPolicyAssignmentsAsync(
            "cache1",
            "rg1",
            "sub123",
            Arg.Any<string>(),
            Arg.Any<AzureMcp.Models.AuthMethod>(),
            Arg.Any<AzureMcp.Arguments.RetryPolicyArguments>())
            .Returns([]);

        var command = new AccessPolicyListCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(["--subscription", "sub123", "--resource-group", "rg1", "--cache", "cache1"]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        _redisService.ListAccessPolicyAssignmentsAsync(
            cacheName: "cache1",
            resourceGroupName: "rg1",
            subscriptionId: "sub123",
            tenant: Arg.Any<string>(),
            authMethod: Arg.Any<AzureMcp.Models.AuthMethod>(),
            retryPolicy: Arg.Any<AzureMcp.Arguments.RetryPolicyArguments>())
            .ThrowsAsync(new Exception("Test error"));

        var command = new AccessPolicyListCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(["--subscription", "sub123", "--resource-group", "rg1", "--cache", "cache1"]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

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
        var command = new AccessPolicyListCommand(_logger);

        var args = new List<string>();
        if (parameterToKeep == "--subscription")
            args.AddRange(["--subscription", "sub123"]);
        if (parameterToKeep == "--resource-group")
            args.AddRange(["--resource-group", "rg1"]);
        if (parameterToKeep == "--cache")
            args.AddRange(["--cache", "cache1"]);

        var parser = new Parser(command.GetCommand());
        var parseResult = parser.Parse(args.ToArray());
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, parseResult);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message, StringComparison.OrdinalIgnoreCase);
    }

    private record AccessPolicyListCommandResult(IEnumerable<AccessPolicyAssignment> AccessPolicyAssignments);
}
