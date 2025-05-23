// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

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
        Assert.Equal("Starts Azure MCP Server.", _command.GetCommand().Description!);
    }
}
