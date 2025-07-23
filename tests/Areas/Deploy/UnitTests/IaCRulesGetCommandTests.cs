// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.Deploy.Commands.InfraCodeRules;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Deploy.UnitTests;

[Trait("Area", "Deploy")]
public class IaCRulesGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<IaCRulesGetCommand> _logger;
    private readonly Parser _parser;
    private readonly CommandContext _context;
    private readonly IaCRulesGetCommand _command;

    public IaCRulesGetCommandTests()
    {
        _logger = Substitute.For<ILogger<IaCRulesGetCommand>>();

        var collection = new ServiceCollection();
        _serviceProvider = collection.BuildServiceProvider();
        _context = new(_serviceProvider);
        _command = new(_logger);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task Should_get_infrastructure_code_rules()
    {
        // arrange
        var args = _parser.Parse([
            "--deployment-tool", "azd",
            "--iac-type", "bicep",
            "--resource-types", "appservice, azurestorage"
        ]);

        // act
        var result = await _command.ExecuteAsync(_context, args);

        // assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.Contains("Deployment Tool azd rules", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_get_infrastructure_rules_for_terraform()
    {
        // arrange
        var args = _parser.Parse([
            "--deployment-tool", "azd",
            "--iac-type", "terraform",
            "--resource-types", "containerapp, azurecosmosdb"
        ]);

        // act
        var result = await _command.ExecuteAsync(_context, args);

        // assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.Contains("Expected parameters in terraform parameters", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_get_infrastructure_rules_for_function_app()
    {
        // arrange
        var args = _parser.Parse([
            "--deployment-tool", "azd",
            "--iac-type", "bicep",
            "--resource-types", "function"
        ]);

        // act
        var result = await _command.ExecuteAsync(_context, args);

        // assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.Contains("Additional requirements for Function Apps", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Storage Blob Data Owner", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_get_infrastructure_rules_for_container_app()
    {
        // arrange
        var args = _parser.Parse([
            "--deployment-tool", "azd",
            "--iac-type", "bicep",
            "--resource-types", "containerapp"
        ]);

        // act
        var result = await _command.ExecuteAsync(_context, args);

        // assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.Contains("Additional requirements for Container Apps", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("mcr.microsoft.com/azuredocs/containerapps-helloworld:latest", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_get_infrastructure_rules_for_azcli_deployment_tool()
    {
        // arrange
        var args = _parser.Parse([
            "--deployment-tool", "AzCli",
            "--iac-type", "bicep",
            "--resource-types", "appservice"
        ]);

        // act
        var result = await _command.ExecuteAsync(_context, args);

        // assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.Contains("Deployment Tool AzCli", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("No additional rules", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_include_necessary_tools_in_response()
    {
        // arrange
        var args = _parser.Parse([
            "--deployment-tool", "azd",
            "--iac-type", "terraform",
            "--resource-types", "containerapp"
        ]);

        // act
        var result = await _command.ExecuteAsync(_context, args);

        // assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.Contains("Tools needed:", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("az cli", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("azd", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("docker", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Should_handle_multiple_resource_types()
    {
        // arrange
        var args = _parser.Parse([
            "--deployment-tool", "azd",
            "--iac-type", "bicep",
            "--resource-types", "appservice,containerapp,function"
        ]);

        // act
        var result = await _command.ExecuteAsync(_context, args);

        // assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.Contains("Resources: appservice, containerapp, function", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("App Service Rules", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Additional requirements for Container Apps", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Additional requirements for Function Apps", result.Message, StringComparison.OrdinalIgnoreCase);
    }
}
