// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Areas.Server.Commands;
using AzureMcp.Models.Option;
using Xunit;

<<<<<<<< HEAD:tests/Areas/Server/UnitTests/ServiceStartCommandTests.cs
namespace AzureMcp.Tests.Areas.Server.UnitTests;
========
namespace AzureMcp.Tests.Areas.Server;
>>>>>>>> fce46ac (Adds unit tests for server discovery and tool loading strategies):tests/Areas/Server/ServiceStartCommandTests.cs

[Trait("Area", "Server")]
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

    [Theory]
    [InlineData(null, "", 1234, "stdio")]
    [InlineData("storage", "storage", 1234, "stdio")]
    public void ServiceOption_ParsesCorrectly(string? inputService, string expectedService, int expectedPort, string expectedTransport)
    {
        // Arrange
        var parseResult = CreateParseResult(inputService);

        // Act
        var actualServiceArray = parseResult.GetValueForOption(OptionDefinitions.Service.ServiceType);
        var actualService = (actualServiceArray != null && actualServiceArray.Length > 0) ? actualServiceArray[0] : "";
        var actualPort = parseResult.GetValueForOption(OptionDefinitions.Service.Port);
        var actualTransport = parseResult.GetValueForOption(OptionDefinitions.Service.Transport);

        // Assert
        Assert.Equal(expectedService, actualService ?? "");
        Assert.Equal(expectedPort, actualPort);
        Assert.Equal(expectedTransport, actualTransport);
    }

    private static ParseResult CreateParseResult(string? serviceValue)
    {
        var root = new RootCommand
        {
            OptionDefinitions.Service.ServiceType,
            OptionDefinitions.Service.Port,
            OptionDefinitions.Service.Transport
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
