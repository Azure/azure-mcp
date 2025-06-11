// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Server;

public class McpServerToolAttributeTests
{
    [Fact]
    public void AllExecuteAsyncMethodsWithMcpServerToolAttribute_ShouldHaveValidTitle()
    {
        // Arrange
        var logger = Substitute.For<ILogger<CommandFactory>>();
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var commandFactory = new CommandFactory(serviceProvider, logger);

        var titleValidationErrors = new List<string>();

        // Act - Get all command types and check their ExecuteAsync methods
        foreach (var (commandName, command) in commandFactory.AllCommands)
        {
            // Get the ExecuteAsync method
            var executeAsyncMethod = command.GetType().GetMethod("ExecuteAsync");

            if (executeAsyncMethod == null)
                continue;

            // Check if the method has the McpServerTool attribute
            var mcpServerToolAttribute = executeAsyncMethod.GetCustomAttribute<McpServerToolAttribute>();

            if (mcpServerToolAttribute == null)
                continue;

            var commandTypeName = command.GetType().FullName;

            // Check 1: Title property must not be null or whitespace
            if (string.IsNullOrWhiteSpace(mcpServerToolAttribute.Title))
            {
                titleValidationErrors.Add($"{commandTypeName}: Missing or empty Title property");
                continue; // Skip further validation if title is null/empty
            }

            var title = mcpServerToolAttribute.Title.Trim();

            // Check 2: Title must not be just whitespace after trimming
            if (title.Length == 0)
            {
                titleValidationErrors.Add($"{commandTypeName}: Title contains only whitespace");
                continue;
            }

            // Check 3: Title should be reasonably descriptive (at least 5 characters)
            if (title.Length < 5)
            {
                titleValidationErrors.Add($"{commandTypeName}: Title too short ('{title}') - should be at least 5 characters");
            }

            // Check 4: Title should not be generic/placeholder
            var genericTitles = new[] { "TODO", "PLACEHOLDER", "FIXME", "TBD", "Command", "Tool" };
            if (genericTitles.Any(generic => title.Equals(generic, StringComparison.OrdinalIgnoreCase)))
            {
                titleValidationErrors.Add($"{commandTypeName}: Title is generic placeholder ('{title}')");
            }
        }

        // Assert
        Assert.True(titleValidationErrors.Count == 0,
            $"The following commands have ExecuteAsync methods with invalid McpServerTool Title properties:\n" +
            string.Join("\n", titleValidationErrors));
    }

    [Fact]
    public void GetListOfAllCommandsWithMcpServerToolAttribute_ForDocumentation()
    {
        // Arrange
        var logger = Substitute.For<ILogger<CommandFactory>>();
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var commandFactory = new CommandFactory(serviceProvider, logger);

        var commandsWithMcpServerTool = new List<(string CommandName, string CommandType, string Title)>();

        // Act - Get all command types and collect those with McpServerTool attribute
        foreach (var (commandName, command) in commandFactory.AllCommands)
        {
            // Get the ExecuteAsync method
            var executeAsyncMethod = command.GetType().GetMethod("ExecuteAsync");

            if (executeAsyncMethod == null)
                continue;

            // Check if the method has the McpServerTool attribute
            var mcpServerToolAttribute = executeAsyncMethod.GetCustomAttribute<McpServerToolAttribute>();

            if (mcpServerToolAttribute == null)
                continue;

            commandsWithMcpServerTool.Add((
                commandName,
                command.GetType().Name,
                mcpServerToolAttribute.Title ?? "[NULL]"
            ));
        }

        // Act - Output for documentation purposes (this test should always pass but provides useful info)
        var documentation = commandsWithMcpServerTool
            .OrderBy(x => x.CommandName)
            .Select(x => $"- {x.CommandName} ({x.CommandType}): \"{x.Title}\"");

        var output = $"Found {commandsWithMcpServerTool.Count} commands with McpServerTool attribute:\n" +
                    string.Join("\n", documentation);

        // Assert - This test is informational and should always pass
        // But we can assert that we found at least some commands with the attribute
        Assert.True(commandsWithMcpServerTool.Count > 0,
            "Expected to find at least one command with McpServerTool attribute");

        // Output the list for documentation/debugging purposes
        // This will show in test output when running with verbose logging
        Console.WriteLine(output);
    }
}
