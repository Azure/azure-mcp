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
public class AzureFunctionsCodeGenBestPracticesGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AzureFunctionsCodeGenBestPracticesGetCommand> _logger;
    private readonly CommandContext _context;
    private readonly AzureFunctionsCodeGenBestPracticesGetCommand _command;
    private readonly Parser _parser;

    public AzureFunctionsCodeGenBestPracticesGetCommandTests()
    {
        var collection = new ServiceCollection();
        _serviceProvider = collection.BuildServiceProvider();

        _context = new(_serviceProvider);
        _logger = Substitute.For<ILogger<AzureFunctionsCodeGenBestPracticesGetCommand>>();
        _command = new(_logger);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsAzureFunctionsCodeGenBestPractices()
    {
        var args = _parser.Parse([]);
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<string[]>(json);

        Assert.NotNull(result);
        Assert.Contains("Use the latest programming models (v4 for JavaScript, v2 for Python)", result[0]);
        Assert.Contains("Azure Functions Core Tools for creating Function Apps", result[0]);
        Assert.Contains("extension bundles version `[4.*, 5.0.0)`", result[0]);
        Assert.Contains("isolated process model over in-process", result[0]);
        Assert.Contains("Provide steps for testing Functions locally", result[0]);
    }
}
