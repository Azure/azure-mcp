// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.Text.Json;
using Azure.ResourceManager.AppService.Models;
using Azure.ResourceManager.Storage.Models;
using AzureMcp.Core.Models.Command;
using AzureMcp.FunctionApp.Commands;
using AzureMcp.FunctionApp.Commands.FunctionApp;
using AzureMcp.FunctionApp.Models;
using AzureMcp.FunctionApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

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
    [InlineData("--subscription sub --resource-group rg --function-app myapp --location eastus", true)]
    [InlineData("--subscription sub --resource-group rg --function-app myapp", false)]
    [InlineData("--subscription sub --location eastus --function-app myapp", false)]
    [InlineData("--resource-group rg --location eastus --function-app myapp", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        if (shouldSucceed)
        {
            _service.CreateFunctionApp(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
                Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
                .Returns(new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null, null));
        }

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse(args);

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsCreatedFunctionApp()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null, null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --function-app myapp --location eastus");

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
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(Task.FromException<FunctionAppInfo>(new Exception("Create error")));

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --function-app myapp --location eastus");

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(500, response.Status);
        Assert.Contains("Create error", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_PassesPlanTypeAndRuntimeVersionToService()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null, null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
                Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);

        var context = new CommandContext(_serviceProvider);
        var args = "--subscription sub --resource-group rg --function-app myapp --location eastus --plan-type flex --runtime node --runtime-version 22";
        var parseResult = _command.GetCommand().Parse(args);

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_DefaultsRuntimeToDotnet_WhenNotProvided()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null, null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --function-app myapp --location eastus");

        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(200, response.Status);
    }

    [Theory]
    [InlineData("", null, null)]
    [InlineData("--plan-type consumption", "consumption", null)]
    [InlineData("--plan-type flex", "flex", null)]
    [InlineData("--plan-type premium", "premium", null)]
    [InlineData("--plan-type appservice", "appservice", null)]
    [InlineData("--plan-sku S1", null, "S1")]
    [InlineData("--plan-type premium --plan-sku EP2", "premium", "EP2")]
    [InlineData("--plan-type containerapp", "containerapp", null)]
    public async Task ExecuteAsync_PlanSelection_Matrix(string argsSuffix, string? expectedPlanType, string? expectedPlanSku)
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", expectedPlanType ?? "plan", "Running", "myapp.azurewebsites.net", null, null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);

        var baseArgs = "--subscription sub --resource-group rg --function-app myapp --location eastus";
        var fullArgs = string.IsNullOrWhiteSpace(argsSuffix) ? baseArgs : $"{baseArgs} {argsSuffix}";
        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse(fullArgs);
        var response = await _command.ExecuteAsync(context, parseResult);

        Assert.Equal(200, response.Status);

        await _service.Received(1).CreateFunctionApp(
                "sub",
                "rg",
                "myapp",
                "eastus",
                Arg.Any<string?>(),
                Arg.Is<string?>(pt => pt == expectedPlanType),
                Arg.Is<string?>(ps => ps == expectedPlanSku),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
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
    [InlineData("node", null, false, "~22")]
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

    [Fact]
    public async Task ExecuteAsync_ExistingPlan_NoPlanTypeOrSku()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "existingPlan", "Running", "myapp.azurewebsites.net", null, null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);
        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --function-app myapp --location eastus --app-service-plan existingPlan");
        var response = await _command.ExecuteAsync(context, parseResult);
        Assert.Equal(200, response.Status);
        await _service.Received(1).CreateFunctionApp(
            "sub", "rg", "myapp", "eastus",
            Arg.Is<string?>(p => p == "existingPlan"),
            Arg.Is<string?>(pt => pt == null),
            Arg.Is<string?>(ps => ps == null),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_SkuPrecedence_OverridesPlanType()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null, null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);
        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --function-app myapp --location eastus --plan-type flex --plan-sku B1");
        var response = await _command.ExecuteAsync(context, parseResult);
        Assert.Equal(200, response.Status);
        await _service.Received(1).CreateFunctionApp(
            "sub", "rg", "myapp", "eastus",
            Arg.Any<string?>(),
            Arg.Is<string?>(pt => pt == "flex"),
            Arg.Is<string?>(ps => ps == "B1"),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_DotnetIsolated_RuntimeAccepted()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null, null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);
        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --function-app myapp --location eastus --runtime dotnet-isolated");
        var response = await _command.ExecuteAsync(context, parseResult);
        Assert.Equal(200, response.Status);
        await _service.Received(1).CreateFunctionApp(
            "sub", "rg", "myapp", "eastus",
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Is<string?>(r => r == "dotnet-isolated"),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_PassesOperatingSystemOption()
    {
        var expected = new FunctionAppInfo("myapp", "rg", "eastus", "plan", "Running", "myapp.azurewebsites.net", null, null);
        _service.CreateFunctionApp(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Is<string?>(os => os == "linux"), Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>())
            .Returns(expected);
        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--subscription sub --resource-group rg --function-app myapp --location eastus --os linux");
        var response = await _command.ExecuteAsync(context, parseResult);
        Assert.Equal(200, response.Status);
        await _service.Received(1).CreateFunctionApp(
            "sub", "rg", "myapp", "eastus",
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Is<string?>(os => os == "linux"),
            Arg.Any<string?>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions?>());
    }

    [Fact]
    public void BuildAppSettings_NodeOnLinux_DoesNotSetWebsiteNodeDefaultVersion()
    {
        var dict = FunctionAppService.BuildAppSettings("node", "22", true, "UseDevelopmentStorage=true");
        Assert.False(dict.Properties.ContainsKey("WEBSITE_NODE_DEFAULT_VERSION"));
    }

    [Fact]
    public void BuildAppSettings_FlexConsumption_OmitsFunctionsWorkerRuntime()
    {
        var dict = FunctionAppService.BuildAppSettings("dotnet", null, false, "UseDevelopmentStorage=true", includeWorkerRuntime: false);
        Assert.False(dict.Properties.ContainsKey("FUNCTIONS_WORKER_RUNTIME"));
        Assert.Equal("~4", dict.Properties["FUNCTIONS_EXTENSION_VERSION"]);
        Assert.Equal("UseDevelopmentStorage=true", dict.Properties["AzureWebJobsStorage"]);
    }

    [Fact]
    public void CreateStorageAccountOptions_Defaults()
    {
        var opts = FunctionAppService.CreateStorageAccountOptions("eastus");
        Assert.Equal(StorageSkuName.StandardLrs, opts.Sku.Name);
        Assert.Equal(StorageKind.StorageV2, opts.Kind);
        Assert.Equal(StorageAccountAccessTier.Hot, opts.AccessTier);
        Assert.True(opts.EnableHttpsTrafficOnly);
        Assert.False(opts.AllowBlobPublicAccess);
        Assert.False(opts.IsHnsEnabled);
    }

    [Theory]
    [InlineData("dotnet", "mcr.microsoft.com/azure-functions/dotnet:4")]
    [InlineData("dotnet-isolated", "mcr.microsoft.com/azure-functions/dotnet-isolated:4")]
    [InlineData("node", "mcr.microsoft.com/azure-functions/node:4")]
    [InlineData("python", "mcr.microsoft.com/azure-functions/python:4")]
    [InlineData("java", "mcr.microsoft.com/azure-functions/java:4")]
    [InlineData("powershell", "mcr.microsoft.com/azure-functions/powershell:4")]
    [InlineData("unknownRuntime", "mcr.microsoft.com/azure-functions/dotnet-isolated:4")]
    public void ResolveFunctionsContainerImage_MapsRuntimes(string runtime, string expectedImage)
    {
        var image = FunctionAppService.ResolveFunctionsContainerImage(runtime);
        Assert.Equal(expectedImage, image);
    }

    [Theory]
    [InlineData("python", null, true)]
    [InlineData("node", "flex", true)]
    [InlineData("dotnet", "flex", true)]
    [InlineData("dotnet", null, false)]
    [InlineData("java", null, false)]
    [InlineData("python", "appservice", true)]
    [InlineData("node", "consumption", false)]
    [InlineData("node", "containerapp", true)]
    public void RequiresLinuxFor_CorrectlyEvaluates(string runtime, string? planType, bool expected)
    {
        var actual = FunctionAppService.RequiresLinuxFor(runtime, planType);
        Assert.Equal(expected, actual);
    }
}
