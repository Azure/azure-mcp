// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.Text.Json;
using AzureMcp.Core.Models.Command;
using AzureMcp.FunctionApp.Commands;
using AzureMcp.FunctionApp.Commands.FunctionApp;
using AzureMcp.FunctionApp.Models;
using AzureMcp.FunctionApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using Azure.ResourceManager.AppService.Models;

namespace AzureMcp.FunctionApp.UnitTests.FunctionApp;

public sealed class FunctionAppCreateCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IFunctionAppService _service;
    private readonly ILogger<FunctionAppCreateCommand> _logger;
    private readonly FunctionAppCreateCommand _command;

    public FunctionAppCreateCommandTests()
    {
        _service = Substitute.For<IFunctionAppService>();
        _logger = Substitute.For<ILogger<FunctionAppCreateCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("create", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--subscription sub --resource-group rg --functionapp myapp --location eastus", true)]
    [InlineData("--subscription sub --resource-group rg --functionapp myapp", false)]
    [InlineData("--subscription sub --location eastus --functionapp myapp", false)]
    [InlineData("--resource-group rg --location eastus --functionapp myapp", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        if (shouldSucceed)
        {
            _service.CreateFunctionApp(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
                .Returns(new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null));
        }

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse(args);

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsCreatedFunctionApp()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --functionapp myapp --location eastus");

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<FunctionAppCreateCommand.FunctionAppCreateCommandResult>(json, FunctionAppJsonContext.Default.FunctionAppCreateCommandResult);
        Assert.NotNull(result);
        Assert.Equal("myapp", result.FunctionApp.Name);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(Task.FromException<FunctionAppInfo>(new Exception("Create error")));

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --functionapp myapp --location eastus");

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(500, response.Status);
        Assert.Contains("Create error", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_PassesPlanTypeAndRuntimeVersionToService()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);

        var context = new CommandContext(_serviceProvider);
        var args = "--subscription sub --resource-group rg --functionapp myapp --location eastus --plan-type flex --runtime node --runtime-version 22";
        var parseResult = _command.GetCommand().Parse(args);

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(200, response.Status);
        await _service.Received(1).CreateFunctionApp(
            Arg.Is("sub"),
            Arg.Is("rg"),
            Arg.Is("myapp"),
            Arg.Is("eastus"),
            Arg.Is<string?>(s => s == null),
            Arg.Is("flex"),
            Arg.Is<string?>(s => s == null),
            Arg.Is("node"),
            Arg.Is("22"),
            Arg.Is<string?>(s => s == null),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_DefaultsRuntimeToDotnet_WhenNotProvided()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --functionapp myapp --location eastus");

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(200, response.Status);
        await _service.Received(1).CreateFunctionApp(
            Arg.Is("sub"),
            Arg.Is("rg"),
            Arg.Is("myapp"),
            Arg.Is("eastus"),
            Arg.Is<string?>(s => s == null),
            Arg.Is<string?>(s => s == null),
            Arg.Is<string?>(s => s == null),
            Arg.Is("dotnet"),
            Arg.Is<string?>(s => s == null),
            Arg.Is<string?>(s => s == null),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>());
    }

    [Theory]
    [InlineData("python", null, "Python|3.12")]
    [InlineData("python", "3.11", "Python|3.11")]
    [InlineData("node", null, "Node|22")]
    [InlineData("node", "20", "Node|20")]
    [InlineData("dotnet", null, "DOTNET|8.0")]
    [InlineData("dotnet", "7.0", "DOTNET|7.0")]
    [InlineData("java", null, "Java|21.0")]
    [InlineData("java", "17.0", "Java|17.0")]
    [InlineData("powershell", null, "PowerShell|7.4")]
    [InlineData("powershell", "7.3", "PowerShell|7.3")]
    public void CreateLinuxSiteConfig_ComposesLinuxFxVersion(string runtime, string? version, string expected)
    {
        SiteConfigProperties? cfg = FunctionAppService.CreateLinuxSiteConfig(runtime, version);
        Assert.NotNull(cfg);
        Assert.Equal(expected, cfg!.LinuxFxVersion);
    }

    [Theory]
    [InlineData("node", "22", false, "~22")]
    [InlineData("node", "22.3.1", false, "~22")]
    [InlineData("node", "22 LTS", false, "~22")]
    [InlineData("node", null, false, null)]
    [InlineData("node", "20", true, null)]
    [InlineData("python", "3.12", false, null)]
    public void BuildAppSettings_ComposesWebsiteNodeDefaultVersion_WhenApplicable(string runtime, string? runtimeVersion, bool requiresLinux, string? expected)
    {
        var dict = FunctionAppService.BuildAppSettings(runtime, runtimeVersion, requiresLinux, "UseDevelopmentStorage=true");

        dict.Properties.TryGetValue("WEBSITE_NODE_DEFAULT_VERSION", out var actualObj);
        var actual = actualObj as string;

        Assert.Equal(expected, actual);
        Assert.Equal(runtime, dict.Properties["FUNCTIONS_WORKER_RUNTIME"]);
        Assert.Equal("~4", dict.Properties["FUNCTIONS_EXTENSION_VERSION"]);
        Assert.Equal("UseDevelopmentStorage=true", dict.Properties["AzureWebJobsStorage"]);
    }
}
