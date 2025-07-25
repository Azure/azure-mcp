// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;

namespace AzureMcp.Core.Commands;

/// <summary>
/// Interface for all commands
/// </summary>
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
public interface IBaseCommand
{
    /// <summary>
    /// Gets the name of the command
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the description of the command
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets the name of the command
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets the command definition
    /// </summary>
    Command GetCommand();

    /// <summary>
    /// Executes the command
    /// </summary>
    Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult);

    ValidationResult Validate(CommandResult commandResult, CommandResponse? commandResponse = null);
}
