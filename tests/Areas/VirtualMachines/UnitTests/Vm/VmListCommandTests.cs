// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.VirtualMachines.Commands.Vm;
using AzureMcp.Areas.VirtualMachines.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.VirtualMachines.UnitTests.Vm;

[Trait("Area", "VirtualMachines")]
public class VmListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IVirtualMachinesService _virtualMachinesService;
    private readonly ILogger<VmListCommand> _logger;
    private readonly VmListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public VmListCommandTests()
    {
        _virtualMachinesService = Substitute.For<IVirtualMachinesService>();
        _logger = Substitute.For<ILogger<VmListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_virtualMachinesService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--subscription sub123", true)]
    [InlineData("--subscription sub123 --resource-group rg1", true)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var expectedVms = new List<VirtualMachineInfo>
            {
                new("vm1", "rg1", "East US", "Running", "Standard_B1s", "Linux"),
                new("vm2", "rg1", "East US", "Stopped", "Standard_B1ms", "Windows")
            };

            _virtualMachinesService.ListVirtualMachinesAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>(),
                Arg.Any<CancellationToken>())
                .Returns(expectedVms);
        }

        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsVirtualMachines_WhenSuccessful()
    {
        // Arrange
        var subscriptionId = "sub123";
        var resourceGroup = "rg1";
        var expectedVms = new List<VirtualMachineInfo>
        {
            new("vm1", "rg1", "East US", "Running", "Standard_B1s", "Linux"),
            new("vm2", "rg1", "East US", "Stopped", "Standard_B1ms", "Windows")
        };

        _virtualMachinesService.ListVirtualMachinesAsync(
            subscriptionId,
            resourceGroup,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedVms);

        var args = _parser.Parse(["--subscription", subscriptionId, "--resource-group", resourceGroup]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<VmListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(2, result.VirtualMachines.Count);
        Assert.Equal("vm1", result.VirtualMachines[0].Name);
        Assert.Equal("vm2", result.VirtualMachines[1].Name);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNull_WhenNoVirtualMachines()
    {
        // Arrange
        var subscriptionId = "sub123";

        _virtualMachinesService.ListVirtualMachinesAsync(
            subscriptionId,
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<CancellationToken>())
            .Returns([]);

        var args = _parser.Parse(["--subscription", subscriptionId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Null(response.Results);
        Assert.Equal(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        var expectedError = "Test error";
        var subscriptionId = "sub123";

        _virtualMachinesService.ListVirtualMachinesAsync(
            subscriptionId,
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<CancellationToken>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse(["--subscription", subscriptionId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains(expectedError, response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    private class VmListResult
    {
        [JsonPropertyName("virtualMachines")]
        public List<VirtualMachineInfo> VirtualMachines { get; set; } = [];
    }
}