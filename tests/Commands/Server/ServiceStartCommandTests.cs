// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using Xunit;

namespace AzureMcp.Commands.Server.Tests;

public class ServiceStartCommandTests
{
    private readonly ServiceStartCommand _command;

    public ServiceStartCommandTests()
    {
        _command = new();
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        // Arrange & Act

        // Assert
        Assert.Equal("start", _command.GetCommand().Name);
        Assert.Equal("Starts Azure MCP Server.", _command.GetCommand().Description);
    }

    [Fact]
    public void GetArguments_ReturnsExpectedArguments()
    {
        // Arrange

        // Act
        var args = _command.GetArguments()?.ToList();

        // Assert
        Assert.NotNull(args);
        Assert.Equal(2, args.Count);
        Assert.Contains(args, a => a.Name == ArgumentDefinitions.Service.Transport.Name);
        Assert.Contains(args, a => a.Name == ArgumentDefinitions.Service.Port.Name);
    }

    [Fact]
    public void ClearArguments_RemovesAllArguments()
    {
        // Act
        _command.ClearArguments();

        // Assert
        var arguments = _command.GetArguments();
        Assert.NotNull(arguments);
        Assert.Empty(arguments);
    }

    [Fact]
    public void AddArgument_AddsNewArgument()
    {
        // Arrange
        var newArg = new ArgumentDefinition<string>("test", "test description");
        var arguments = _command.GetArguments();

        Assert.NotNull(arguments);

        var initialCount = arguments.Count();

        // Act
        _command.AddArgument(newArg);

        // Assert
        Assert.Equal(initialCount + 1, arguments.Count());
        Assert.Contains(arguments, a => a.Name == "test");
    }
}
