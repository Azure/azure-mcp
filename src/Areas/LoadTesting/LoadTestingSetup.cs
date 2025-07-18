// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


using AzureMcp.Areas.LoadTesting.Commands.LoadTest;
using AzureMcp.Areas.LoadTesting.Commands.LoadTestResource;
using AzureMcp.Areas.LoadTesting.Commands.LoadTestRun;
using AzureMcp.Areas.LoadTesting.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.LoadTesting;

public class LoadTestingSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILoadTestingService, LoadTestingService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Load Testing command group
        var service = new CommandGroup(
            "loadtesting",
            "Load Testing operations - Commands for managing Azure Load Testing resources.");
        rootGroup.AddSubGroup(service);

        // Create Load Test subgroups
        var testResource = new CommandGroup(
            "testresource",
            "Load test resource operations - Commands for listing, creating and managing Azure load test resources.");
        service.AddSubGroup(testResource);

        var test = new CommandGroup(
            "test",
            "Load test operations - Commands for listing, creating and managing Azure load tests.");
        service.AddSubGroup(test);

        var testRun = new CommandGroup(
            "testrun",
            "Load test run operations - Commands for listing, creating and managing Azure load test runs.");
        service.AddSubGroup(testRun);

        // Register commands for Load Test Resource
        testResource.AddCommand("list", new TestResourceListCommand(loggerFactory.CreateLogger<TestResourceListCommand>()));
        testResource.AddCommand("create", new TestResourceCreateCommand(loggerFactory.CreateLogger<TestResourceCreateCommand>()));

        // Register commands for Load Test
        test.AddCommand("get", new TestGetCommand(loggerFactory.CreateLogger<TestGetCommand>()));
        test.AddCommand("create", new TestCreateCommand(loggerFactory.CreateLogger<TestCreateCommand>()));

        // Register commands for Load Test Run
        testRun.AddCommand("get", new TestRunGetCommand(loggerFactory.CreateLogger<TestRunGetCommand>()));
        testRun.AddCommand("list", new TestRunListCommand(loggerFactory.CreateLogger<TestRunListCommand>()));
        testRun.AddCommand("create", new TestRunCreateCommand(loggerFactory.CreateLogger<TestRunCreateCommand>()));
        testRun.AddCommand("update", new TestRunUpdateCommand(loggerFactory.CreateLogger<TestRunUpdateCommand>()));
    }
}
