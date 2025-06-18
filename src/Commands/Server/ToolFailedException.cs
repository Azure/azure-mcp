// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Commands.Server;

public class ToolFailedException : Exception
{
    public ToolFailedException(string toolName, string message) : base(message)
    {
        ToolName = toolName;
    }

    public string ToolName { get; }
}
