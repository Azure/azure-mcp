// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Areas.AzureBestPractices.Commands;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.AzureBestPractices.UnitTests;

[Trait("Area", "AzureBestPractices")]
public class AzureFunctionsDeploymentBestPracticesGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AzureFunctionsDeploymentBestPracticesGetCommand> _logger;
    private readonly CommandContext _context;
    private readonly AzureFunctionsDeploymentBestPracticesGetCommand _command;
    private readonly Parser _parser;

    public AzureFunctionsDeploymentBestPracticesGetCommandTests()
    {
        var collection = new ServiceCollection();
        _serviceProvider = collection.BuildServiceProvider();

        _context = new(_serviceProvider);
        _logger = Substitute.For<ILogger<AzureFunctionsDeploymentBestPracticesGetCommand>>();
        _command = new(_logger);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsAzureFunctionsDeploymentBestPractices()
    {
        var args = _parser.Parse([]);
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<string[]>(json);

        Assert.NotNull(result);
        Assert.Contains("Default hosting plan is flex consumption plan.", result[0]);
        Assert.Contains("Always use Linux OS for Python-based Functions", result[0]);
        Assert.Contains("Configure Function authentication", result[0]);
        Assert.Contains("private networking options such as Virtual Network", result[0]);
        Assert.Contains("Always enable Application Insights", result[0]);
    }
}
