// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Server.Options;

public static class ServerOptionDefinitions
{
    public const string TransportName = "transport";
    public const string PortName = "port";
    public const string ServiceName = "service";

    public static readonly Option<string> Transport = new(
        $"--{TransportName}",
        () => TransportTypes.StdIo,
        "Transport mechanism to use for Azure MCP Server."
    )
    {
        IsRequired = false
    };

    public static readonly Option<int> Port = new(
        $"--{PortName}",
        () => 5008,
        "Port to use for Azure MCP Server."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string?> ServiceType = new(
        $"--{ServiceName}",
        () => null,
        "The service to expose on the MCP server."
    )
    {
        IsRequired = false,
    };
}
