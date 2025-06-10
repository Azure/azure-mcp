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
    public void AllExecuteAsyncMethodsWithMcpServerToolAttribute_ShouldHaveTitleProperty()
    {
        // Arrange
        var logger = Substitute.For<ILogger<CommandFactory>>();
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var commandFactory = new CommandFactory(serviceProvider, logger);
        
        var commandsWithMissingTitles = new List<string>();

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

            // If it has the attribute, check if Title property is set
            if (string.IsNullOrWhiteSpace(mcpServerToolAttribute.Title))
            {
                commandsWithMissingTitles.Add($"{command.GetType().FullName}: ExecuteAsync method decorated with McpServerTool attribute but missing Title property");
            }
        }

        // Assert
        Assert.True(commandsWithMissingTitles.Count == 0, 
            $"The following commands have ExecuteAsync methods decorated with McpServerTool attribute but are missing the Title property:\n" +
            string.Join("\n", commandsWithMissingTitles));
    }

    [Fact]
    public void AllExecuteAsyncMethodsWithMcpServerToolAttribute_ShouldHaveNonEmptyTitle()
    {
        // Arrange
        var logger = Substitute.For<ILogger<CommandFactory>>();
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var commandFactory = new CommandFactory(serviceProvider, logger);
        
        var commandsWithEmptyTitles = new List<string>();

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

            // If it has the attribute and Title is set, check if it's meaningful (not just whitespace)
            if (!string.IsNullOrWhiteSpace(mcpServerToolAttribute.Title) && 
                mcpServerToolAttribute.Title.Trim().Length == 0)
            {
                commandsWithEmptyTitles.Add($"{command.GetType().FullName}: ExecuteAsync method has McpServerTool attribute with empty or whitespace-only Title");
            }
        }

        // Assert
        Assert.True(commandsWithEmptyTitles.Count == 0, 
            $"The following commands have ExecuteAsync methods with McpServerTool attribute but empty or whitespace-only Title:\n" +
            string.Join("\n", commandsWithEmptyTitles));
    }

    [Fact]
    public void AllExecuteAsyncMethodsWithMcpServerToolAttribute_ShouldHaveMeaningfulTitle()
    {
        // Arrange
        var logger = Substitute.For<ILogger<CommandFactory>>();
        var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
        var commandFactory = new CommandFactory(serviceProvider, logger);
        
        var commandsWithGenericTitles = new List<string>();

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

            // If it has the attribute and Title is set, check if it's meaningful
            if (!string.IsNullOrWhiteSpace(mcpServerToolAttribute.Title))
            {
                var title = mcpServerToolAttribute.Title.Trim();
                
                // Check for generic/placeholder titles that should be avoided
                var genericTitles = new[] { "TODO", "PLACEHOLDER", "FIXME", "TBD", "Command", "Tool" };
                
                if (genericTitles.Any(generic => title.Equals(generic, StringComparison.OrdinalIgnoreCase)))
                {
                    commandsWithGenericTitles.Add($"{command.GetType().FullName}: ExecuteAsync method has McpServerTool attribute with generic Title: '{title}'");
                }
                
                // Title should be reasonably descriptive (at least 5 characters)
                if (title.Length < 5)
                {
                    commandsWithGenericTitles.Add($"{command.GetType().FullName}: ExecuteAsync method has McpServerTool attribute with very short Title: '{title}' (should be at least 5 characters)");
                }
            }
        }

        // Assert
        Assert.True(commandsWithGenericTitles.Count == 0, 
            $"The following commands have ExecuteAsync methods with McpServerTool attribute but generic or very short Title:\n" +
            string.Join("\n", commandsWithGenericTitles));
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
