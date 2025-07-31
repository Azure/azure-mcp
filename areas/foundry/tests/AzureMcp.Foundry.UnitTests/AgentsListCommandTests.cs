// // Copyright (c) Microsoft Corporation.
// // Licensed under the MIT License.
//
// using System.CommandLine;
// using Azure.AI.Agents.Persistent;
// using AzureMcp.Areas.Foundry.Commands.Evaluation;
// using AzureMcp.Areas.Foundry.Services;
// using AzureMcp.Models.Command;
// using AzureMcp.Options;
// using Microsoft.Extensions.DependencyInjection;
// using NSubstitute;
// using NSubstitute.ExceptionExtensions;
// using Xunit;
//
// namespace AzureMcp.Tests.Areas.Foundry.UnitTests;
//
// public class AgentsListCommandTests
// {
//     private readonly IServiceProvider _serviceProvider;
//     private readonly IFoundryService _foundryService;
//
//     public AgentsListCommandTests()
//     {
//         _foundryService = Substitute.For<IFoundryService>();
//
//         var collection = new ServiceCollection();
//         collection.AddSingleton(_foundryService);
//
//         _serviceProvider = collection.BuildServiceProvider();
//     }
//
//     [Fact]
//     public async Task ExecuteAsync_ReturnsAgentsList_WhenAgentsExist()
//     {
//         var endpoint = "https://test-endpoint.azure.com";
//
//         var agent1 = Substitute.For<PersistentAgent>();
//         agent1.Id.Returns("agent-1");
//         agent1.Name.Returns("Test Agent 1");
//         agent1.Description.Returns("This is test agent 1");
//
//         var agent2 = Substitute.For<PersistentAgent>();
//         agent2.Id.Returns("agent-2");
//         agent2.Name.Returns("Test Agent 2");
//         agent2.Description.Returns("This is test agent 2");
//
//         var expectedAgents = new List<PersistentAgent> { agent1, agent2 };
//
//         _foundryService.ListAgents(
//                 Arg.Is<string>(s => s == endpoint),
//                 Arg.Any<string?>(),
//                 Arg.Any<RetryPolicyOptions?>())
//             .Returns(expectedAgents);
//
//         var command = new AgentsListCommand();
//         var args = command.GetCommand().Parse(["--endpoint", endpoint]);
//         var context = new CommandContext(_serviceProvider);
//         var response = await command.ExecuteAsync(context, args);
//
//         Assert.NotNull(response);
//         Assert.NotNull(response.Results);
//         Assert.Equal(200, response.Status);
//     }
//
//     [Fact]
//     public async Task ExecuteAsync_ReturnsNullResults_WhenNoAgentsExist()
//     {
//         var endpoint = "https://test-endpoint.azure.com";
//         var emptyAgentList = new List<PersistentAgent>();
//
//         _foundryService.ListAgents(
//                 Arg.Is<string>(s => s == endpoint),
//                 Arg.Any<string?>(),
//                 Arg.Any<RetryPolicyOptions?>())
//             .Returns(emptyAgentList);
//
//         var command = new AgentsListCommand();
//         var args = command.GetCommand().Parse(["--endpoint", endpoint]);
//         var context = new CommandContext(_serviceProvider);
//         var response = await command.ExecuteAsync(context, args);
//
//         Assert.NotNull(response);
//         Assert.Null(response.Results);
//         Assert.Equal(200, response.Status);
//     }
//
//     [Fact]
//     public async Task ExecuteAsync_HandlesException()
//     {
//         var endpoint = "https://test-endpoint.azure.com";
//         var expectedError = "Failed to list evaluation agents: Service unavailable";
//
//         _foundryService.ListAgents(
//                 Arg.Any<string>(),
//                 Arg.Any<string?>(),
//                 Arg.Any<RetryPolicyOptions?>())
//             .ThrowsAsync(new Exception(expectedError));
//
//         var command = new AgentsListCommand();
//         var args = command.GetCommand().Parse(["--endpoint", endpoint]);
//         var context = new CommandContext(_serviceProvider);
//         var response = await command.ExecuteAsync(context, args);
//
//         Assert.NotNull(response);
//         Assert.Equal(500, response.Status);
//         Assert.StartsWith(expectedError, response.Message);
//     }
//
//     [Fact]
//     public async Task ExecuteAsync_WithMissingEndpoint_ReturnsValidationError()
//     {
//         var command = new AgentsListCommand();
//         var args = command.GetCommand().Parse([]);
//         var context = new CommandContext(_serviceProvider);
//         var response = await command.ExecuteAsync(context, args);
//
//         Assert.NotNull(response);
//         Assert.NotEqual(200, response.Status);
//         Assert.Contains("endpoint", response.Message.ToLower());
//     }
// }
