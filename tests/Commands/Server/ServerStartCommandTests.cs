// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Areas.Server.Commands;
using AzureMcp.Areas.Server.Options;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class ServerStartCommandTests
{
    private readonly ServerStartCommand _command;

    public ServerStartCommandTests()
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

    [Theory]
    [InlineData(null, "", 1234, "stdio")]
    [InlineData("storage", "storage", 1234, "stdio")]
    public void ServiceOption_ParsesCorrectly(string? inputService, string expectedService, int expectedPort, string expectedTransport)
    {
        // Arrange
        var parseResult = CreateParseResult(inputService);

        // Act
        var actualService = parseResult.GetValueForOption(ServerOptionDefinitions.ServiceType);
        var actualPort = parseResult.GetValueForOption(ServerOptionDefinitions.Port);
        var actualTransport = parseResult.GetValueForOption(ServerOptionDefinitions.Transport);

        // Assert
        Assert.Equal(expectedService, actualService ?? "");
        Assert.Equal(expectedPort, actualPort);
        Assert.Equal(expectedTransport, actualTransport);
    }

    private static ParseResult CreateParseResult(string? serviceValue)
    {
        var root = new RootCommand
        {
            ServerOptionDefinitions.ServiceType,
            ServerOptionDefinitions.Port,
            ServerOptionDefinitions.Transport
        };
        var args = new List<string>();
        if (!string.IsNullOrEmpty(serviceValue))
        {
            args.Add("--service");
            args.Add(serviceValue);
        }
        // Add required port/transport defaults for test
        args.Add("--port");
        args.Add("1234");
        args.Add("--transport");
        args.Add("stdio");
        return new Parser(root).Parse(args.ToArray());
    }
}
