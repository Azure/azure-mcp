// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands;

/// <summary>
/// Interface for all commands
/// </summary>
public interface IBaseCommand
{
    /// <summary>
    /// Gets the command definition
    /// </summary>
    Command GetCommand();

    /// <summary>
    /// Executes the command
    /// </summary>
    Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions);

    /// <summary>
    /// Gets the current arguments
    /// </summary>
    IEnumerable<ArgumentDefinition<string>>? GetArguments();

    /// <summary>
    /// Clears the current arguments
    /// </summary>
    void ClearArguments();

    /// <summary>
    /// Adds an argument
    /// </summary>
    void AddArgument(ArgumentDefinition<string> argument);
}