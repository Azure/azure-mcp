// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Areas.Server.Commands;
using AzureMcp.Areas.Server.Options;
using AzureMcp.Models.Option;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests;

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
        var actualServiceArray = parseResult.GetValueForOption(OptionDefinitions.Service.Namespace);
        var actualService = (actualServiceArray != null && actualServiceArray.Length > 0) ? actualServiceArray[0] : "";
        var actualPort = parseResult.GetValueForOption(OptionDefinitions.Service.Port);
        var actualTransport = parseResult.GetValueForOption(OptionDefinitions.Service.Transport);

        // Assert
        Assert.Equal(expectedService, actualService ?? "");
        Assert.Equal(expectedPort, actualPort);
        Assert.Equal(expectedTransport, actualTransport);
    }

    [Theory]
    [InlineData("single", "single")]
    [InlineData("namespace", "namespace")]
    [InlineData("unified-tool", "single")]
    [InlineData("per-service", "namespace")]
    public void ModeTypes_GetCanonicalMode_MapsCorrectly(string inputMode, string expectedCanonicalMode)
    {
        // Act
        var actualMode = ModeTypes.GetCanonicalMode(inputMode);

        // Assert
        Assert.Equal(expectedCanonicalMode, actualMode);
    }

    [Theory]
    [InlineData("storage", "storage")]
    [InlineData("keyvault", "keyvault")]
    [InlineData("storage keyvault", "storage", "keyvault")]
    public void ServiceAreasOption_ParsesCorrectly(string inputServices, params string[] expectedServices)
    {
        // Arrange
        var parseResult = CreateServiceAreasParseResult(inputServices.Split(' '));

        // Act
        var actualServices = parseResult.GetValueForOption(OptionDefinitions.Service.ServiceAreas);

        // Assert
        Assert.NotNull(actualServices);
        Assert.Equal(expectedServices.Length, actualServices.Length);
        for (int i = 0; i < expectedServices.Length; i++)
        {
            Assert.Equal(expectedServices[i], actualServices[i]);
        }
    }

    [Theory]
    [InlineData("unified-tool")]
    [InlineData("per-service")]
    public void ToolGroupingOption_ParsesCorrectly(string inputMode)
    {
        // Arrange
        var parseResult = CreateToolGroupingParseResult(inputMode);

        // Act
        var actualMode = parseResult.GetValueForOption(OptionDefinitions.Service.ToolGrouping);

        // Assert
        Assert.Equal(inputMode, actualMode);
    }

    [Fact]
    public void ModeTypes_AllModeValues_ContainsExpectedValues()
    {
        // Act
        var allModes = ModeTypes.AllModeValues;

        // Assert
        Assert.Contains("single", allModes);
        Assert.Contains("namespace", allModes);
        Assert.Contains("unified-tool", allModes);
        Assert.Contains("per-service", allModes);
    }

    [Fact]
    public void ServiceOptions_ContainsBothTraditionalAndDescriptiveOptions()
    {
        // Arrange
        var command = _command.GetCommand();

        // Act & Assert - Check that both traditional and new options are registered
        var options = command.Options.Select(o => o.Name).ToList();
        
        Assert.Contains("namespace", options);
        Assert.Contains("service-areas", options);
        Assert.Contains("mode", options);
        Assert.Contains("tool-grouping", options);
    }

    private static ParseResult CreateParseResult(string? serviceValue)
    {
        var root = new RootCommand
        {
            OptionDefinitions.Service.Namespace,
            OptionDefinitions.Service.Port,
            OptionDefinitions.Service.Transport
        };
        var args = new List<string>();
        if (!string.IsNullOrEmpty(serviceValue))
        {
            args.Add("--namespace");
            args.Add(serviceValue);
        }
        // Add required port/transport defaults for test
        args.Add("--port");
        args.Add("1234");
        args.Add("--transport");
        args.Add("stdio");
        return new Parser(root).Parse(args.ToArray());
    }

    private static ParseResult CreateServiceAreasParseResult(string[] serviceValues)
    {
        var root = new RootCommand
        {
            OptionDefinitions.Service.ServiceAreas
        };
        var args = new List<string> { "--service-areas" };
        args.AddRange(serviceValues);
        return new Parser(root).Parse(args.ToArray());
    }

    private static ParseResult CreateToolGroupingParseResult(string modeValue)
    {
        var root = new RootCommand
        {
            OptionDefinitions.Service.ToolGrouping
        };
        var args = new List<string> { "--tool-grouping", modeValue };
        return new Parser(root).Parse(args.ToArray());
    }
}
