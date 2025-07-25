// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Core.Commands;

/// <summary>
/// Provides metadata about an MCP tool describing its behavioral characteristics.
/// This metadata helps MCP clients understand how the tool operates and its potential effects.
/// </summary>
public sealed class ToolMetadata
{
    /// <summary>
    /// Gets or sets whether the tool may perform destructive updates to its environment.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <see langword="true"/>, the tool may perform destructive updates to its environment.
    /// If <see langword="false"/>, the tool performs only additive updates.
    /// This property is most relevant when the tool modifies its environment (ReadOnly = false).
    /// </para>
    /// <para>
    /// The default is <see langword="true"/>.
    /// </para>
    /// </remarks>
    public bool Destructive { get; init; } = true;

    /// <summary>
    /// Gets or sets whether calling the tool repeatedly with the same arguments 
    /// will have no additional effect on its environment.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This property is most relevant when the tool modifies its environment (ReadOnly = false).
    /// </para>
    /// <para>
    /// The default is <see langword="false"/>.
    /// </para>
    /// </remarks>
    public bool Idempotent { get; init; } = false;

    /// <summary>
    /// Gets or sets whether this tool may interact with an "open world" of external entities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <see langword="true"/>, the tool may interact with an unpredictable or dynamic set of entities (like web search).
    /// If <see langword="false"/>, the tool's domain of interaction is closed and well-defined (like memory access).
    /// </para>
    /// <para>
    /// The default is <see langword="true"/>.
    /// </para>
    /// </remarks>
    public bool OpenWorld { get; init; } = true;

    /// <summary>
    /// Gets or sets whether this tool does not modify its environment.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <see langword="true"/>, the tool only performs read operations without changing state.
    /// If <see langword="false"/>, the tool may make modifications to its environment.
    /// </para>
    /// <para>
    /// Read-only tools do not have side effects beyond computational resource usage.
    /// They don't create, update, or delete data in any system.
    /// </para>
    /// <para>
    /// The default is <see langword="false"/>.
    /// </para>
    /// </remarks>
    public bool ReadOnly { get; init; } = false;

    /// <summary>
    /// Creates a new instance of <see cref="ToolMetadata"/> with default values.
    /// All properties default to their MCP specification defaults.
    /// </summary>
    public ToolMetadata()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="ToolMetadata"/> with the specified values.
    /// </summary>
    /// <param name="destructive">Whether the command performs destructive operations.</param>
    /// <param name="readOnly">Whether the command only performs read operations.</param>
    /// <param name="idempotent">Whether the command is idempotent.</param>
    /// <param name="openWorld">Whether the command operates in an open world context.</param>
    public ToolMetadata(bool destructive = true, bool readOnly = false, bool idempotent = false, bool openWorld = true)
    {
        Destructive = destructive;
        ReadOnly = readOnly;
        Idempotent = idempotent;
        OpenWorld = openWorld;
    }
}
