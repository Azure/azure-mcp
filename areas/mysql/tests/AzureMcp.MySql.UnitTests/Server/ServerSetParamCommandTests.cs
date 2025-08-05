// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.MySql.Commands.Server;
using AzureMcp.MySql.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.MySql.UnitTests.Server;

public class ServerSetParamCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMySqlService _mysqlService;
    private readonly ILogger<ServerSetParamCommand> _logger;

    public ServerSetParamCommandTests()
    {
        _mysqlService = Substitute.For<IMySqlService>();
        _logger = Substitute.For<ILogger<ServerSetParamCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_mysqlService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_SetsParameter_WhenSuccessful()
    {
        var newValue = "100";
        _mysqlService.SetServerParameterAsync("sub123", "rg1", "user1", "test-server", "max_connections", newValue).Returns(newValue);

        var command = new ServerSetParamCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--resource-group", "rg1",
            "--username", "user1",
            "--server", "test-server",
            "--param", "max_connections",
            "--value", newValue
        ]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ServerSetParamResult>(json);
        Assert.NotNull(result);
        Assert.Equal("max_connections", result.Parameter);
        Assert.Equal(newValue, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsError_WhenServiceThrows()
    {
        _mysqlService.SetServerParameterAsync("sub123", "rg1", "user1", "test-server", "invalid_param", "100")
            .ThrowsAsync(new Exception("Parameter 'invalid_param' not found."));

        var command = new ServerSetParamCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--resource-group", "rg1",
            "--username", "user1",
            "--server", "test-server",
            "--param", "invalid_param",
            "--value", "100"
        ]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Parameter 'invalid_param' not found", response.Message);
    }

    [Fact]
    public void Metadata_IsConfiguredCorrectly()
    {
        var command = new ServerSetParamCommand(_logger);
        
        Assert.Equal("setparam", command.Name);
        Assert.Equal("Sets a specific parameter of a MySQL server.", command.Description);
        Assert.Equal("Set MySQL Server Parameter", command.Title);
        Assert.True(command.Metadata.Destructive);
        Assert.False(command.Metadata.ReadOnly);
    }

    private class ServerSetParamResult
    {
        [JsonPropertyName("Parameter")]
        public string Parameter { get; set; } = string.Empty;

        [JsonPropertyName("Value")]
        public string Value { get; set; } = string.Empty;
    }
}
