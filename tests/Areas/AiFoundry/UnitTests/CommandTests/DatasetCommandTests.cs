using AzureMcp.Areas.AiFoundry.Commands;
using AzureMcp.Areas.AiFoundry.Models;
using AzureMcp.Areas.AiFoundry.Options;
using AzureMcp.Areas.AiFoundry.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.CommandLine.Parsing;
using Xunit;

namespace AzureMcp.Tests.Areas.AiFoundry.UnitTests.CommandTests;

public class DatasetCommandTests
{
    private readonly ILogger<DatasetListCommand> _mockListLogger;
    private readonly ILogger<DatasetDescribeCommand> _mockDescribeLogger;

    public DatasetCommandTests()
    {
        _mockListLogger = Substitute.For<ILogger<DatasetListCommand>>();
        _mockDescribeLogger = Substitute.For<ILogger<DatasetDescribeCommand>>();
    }

    [Fact]
    public void DatasetListCommand_HasCorrectName()
    {
        // Arrange & Act
        var command = new DatasetListCommand(_mockListLogger);

        // Assert
        Assert.Equal("list", command.Name);
    }

    [Fact]
    public void DatasetListCommand_HasCorrectTitle()
    {
        // Arrange & Act
        var command = new DatasetListCommand(_mockListLogger);

        // Assert
        Assert.Equal("List AI Foundry Datasets", command.Title);
    }

    [Fact]
    public void DatasetDescribeCommand_HasCorrectName()
    {
        // Arrange & Act
        var command = new DatasetDescribeCommand(_mockDescribeLogger);

        // Assert
        Assert.Equal("describe", command.Name);
    }

    [Fact]
    public void DatasetDescribeCommand_HasCorrectTitle()
    {
        // Arrange & Act
        var command = new DatasetDescribeCommand(_mockDescribeLogger);

        // Assert
        Assert.Equal("Describe AI Foundry Dataset", command.Title);
    }
} 