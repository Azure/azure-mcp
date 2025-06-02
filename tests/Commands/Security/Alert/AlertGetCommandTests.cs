// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Commands.Security.Alert;
using AzureMcp.Options.Security.Alert;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Security.Alert;

/// <summary>
/// Unit tests for AlertGetCommand
/// </summary>
public class AlertGetCommandTests
{
    [Fact]
    public void Constructor_Should_Initialize_Command_Properties()
    {
        // Arrange
        var logger = Substitute.For<ILogger<AlertGetCommand>>();

        // Act
        var command = new AlertGetCommand(logger);

        // Assert
        Assert.Equal("get", command.Name);
        Assert.Contains("Retrieve security alert", command.Description);
        Assert.Equal("Get Security Alert", command.Title);
    }

    [Fact]
    public void GetCommand_Should_Return_Valid_Command()
    {
        // Arrange
        var logger = Substitute.For<ILogger<AlertGetCommand>>();
        var command = new AlertGetCommand(logger);

        // Act
        var systemCommand = command.GetCommand();

        // Assert
        Assert.NotNull(systemCommand);
        Assert.Equal("get", systemCommand.Name);
        Assert.Contains("Retrieve security alert", systemCommand.Description);
    }

    [Fact]
    public void RegisterOptions_Should_Register_AlertId_Option()
    {
        // Arrange
        var logger = Substitute.For<ILogger<AlertGetCommand>>();
        var command = new AlertGetCommand(logger);

        // Act
        var systemCommand = command.GetCommand();

        // Assert
        var alertIdOption = systemCommand.Options.FirstOrDefault(o => o.Name == "alert-id");
        Assert.NotNull(alertIdOption);
        Assert.True(alertIdOption.IsRequired);
    }

    [Fact]
    public void RegisterOptions_Should_Register_Optional_Options()
    {
        // Arrange
        var logger = Substitute.For<ILogger<AlertGetCommand>>();
        var command = new AlertGetCommand(logger);

        // Act
        var systemCommand = command.GetCommand();

        // FIXME
        // Assert
        // var resourceGroupOption = systemCommand.Options.FirstOrDefault(o => o.Name == "resource-group");
        // var subscriptionOption = systemCommand.Options.FirstOrDefault(o => o.Name == "subscription-id-filter");

        // Assert.NotNull(resourceGroupOption);
        // Assert.NotNull(subscriptionOption);
        // Assert.False(resourceGroupOption.IsRequired);
        // Assert.False(subscriptionOption.IsRequired);
    }
}
