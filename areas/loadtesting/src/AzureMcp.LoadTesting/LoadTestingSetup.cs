// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.LoadTesting.Commands.LoadTest;
using AzureMcp.LoadTesting.Commands.LoadTestResource;
using AzureMcp.LoadTesting.Commands.LoadTestRun;
using AzureMcp.LoadTesting.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.LoadTesting;

public class LoadTestingSetup : IAreaSetup
{
    public string Name => "loadtesting";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ILoadTestingService, LoadTestingService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Load Testing command group
        var service = new CommandGroup(
            Name,
            "Load Testing operations - Commands for managing Azure Load Testing service for high-scale performance testing of web applications, APIs, and microservices using JMeter-compatible, Locust test scripts and URLs. Use this tool when you need to create load testing resources, configure performance test scenarios, monitor test execution progress, or analyze performance test results for applications under load. This tool supports load testing with configurable virtual users and duration. Do not use this tool for unit testing, integration testing, Azure Monitor performance monitoring, or application debugging - this tool focuses specifically on load and performance testing scenarios with simulated traffic. This tool is a hierarchical MCP command router where sub-commands are routed to MCP servers that require specific fields inside the parameters object. To invoke a command, set command and wrap its arguments in parameters. Set learn=true to discover available sub-commands for different load testing operations including test creation, execution, and result analysis. Note that this tool requires appropriate Azure Load Testing permissions and may incur costs based on test scale and duration. Currently we support up to 400 engines and consider > 10 engines as a high scale run. Please run tests on a low scale before running high scale tests to avoid high cost implications.");
        rootGroup.AddSubGroup(service);

        // Create Load Test subgroups
        var testResource = new CommandGroup(
            "testresource",
            "Load test resource operations - Manage Azure Load Testing resources (list, show, create). " +
            "Commands:\n  list  - List Load Testing resources in a subscription or resource group.\n  show  - Show resource metadata (includes DataPlaneUri used by test & run commands).\n  create - Create or update a Load Testing resource (returns DataPlaneUri and metadata).");

        service.AddSubGroup(testResource);

        var test = new CommandGroup(
            "test",
            "Load test operations - Manage test definitions on a Load Testing resource (list, get, create). " +
            "Commands:\n  list   - List tests defined on a Load Testing resource.\n  get    - Get a test configuration (duration, ramp-up, virtual users, endpoint).\n  create - Create or update a test definition (duration minutes, virtual-users, ramp-up minutes, endpoint URL).");

        service.AddSubGroup(test);

        var testRun = new CommandGroup(
            "testrun",
            "Load test run operations - Manage test runs and their results (create, list, get, update). " +
            "Commands:\n  create - Start a test run for a test id (async; supports debug-mode and old-testrun-id for reruns).\n  list   - List runs for a test id.\n  get    - Get run details and artifacts.\n  update - Update run metadata or re-run using an existing run id. " +
            "Note: runs are long-running operations and may incur cost; use --debug-mode to capture request-level data.");

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
