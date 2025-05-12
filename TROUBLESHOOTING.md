# Troubleshooting

## Observability with OpenTelemetry

The server supports observability with [OpenTelemetry](https://opentelemetry.io/).

To export telemetry to an OTLP endpoint set the `OTEL_DISABLE_SDK` environment variable to `false`. By default, when OpenTelemetry is enabled, the
server exports telemetry using the default gRPC endpoint at `localhost:4317`. See the [OTLP exporter documentation](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/README.md) for optional configuration details.

You can try it out locally with the [standalone Aspire dashboard](https://learn.microsoft.com/dotnet/aspire/fundamentals/dashboard/standalone):

```bash
docker run --rm -it -d \
    -p 18888:18888 \
    -p 4317:18889 \
    --name aspire-dashboard \
    mcr.microsoft.com/dotnet/aspire-dashboard:9.0
```

To export telemetry to Azure Monitor, set the `APPLICATIONINSIGHTS_CONNECTION_STRING` environment variable.

![image](/docs/images/mcp-trace-aspire.png)

## Logging

The Azure MCP Server is instrumented at various levels of detail using the .NET [EventSource](https://learn.microsoft.com/dotnet/api/system.diagnostics.tracing.eventsource) to emit information. Logging is performed for each operation and follows the pattern of marking the starting point of the operation, its completion, and any exceptions encountered. These logs are invaluable for diagnosing issues that may arise from using the Azure MCP Server.

Server logs can be obtained by capturing events for provider "Microsoft-Extensions-Logging".

### Collecting logs with dotnet-trace

`dotnet-trace` is a cross-platform CLI that enables the collection of .NET Core traces. To collect traces:

1. Install [dotnet-trace](https://learn.microsoft.com/dotnet/core/diagnostics/dotnet-trace).
2. Find the process ID for the server, azmcp.exe.
3. Run: `dotnet-trace collect -p {your-process-id} --providers 'Microsoft-Extensions-Logging:4:5'`
4. Collect the trace.
5. A `.nettrace` file will be output.

On Windows, use [PerfView](https://github.com/Microsoft/perfview) to visualize the `.nettrace` file. In other operating systems, `.nettrace` files can be visualized using third party tools.

For more information about using [dotnet-trace](https://learn.microsoft.com/dotnet/core/diagnostics/dotnet-trace) and valid arguments for `--providers`, see: [Logging in .NET Core and ASP.NET Core: Event Source](https://learn.microsoft.com/aspnet/core/fundamentals/logging#event-source) and [Well-known event providers in .NET](https://learn.microsoft.com/dotnet/core/diagnostics/well-known-event-providers)

### Collecting logs with VS Code

By default, VS Code logs informational, warning, and error level messages. To get a detailed view of the interactions between VS Code and Azure MCP Server:

1. Open Command Palette \(Ctrl+Shift+P\).
2. Search for "MCP: List Servers".
3. Select "Azure MCP Server".
4. Select "Show Output".
5. Examine the "Output" window in VS Code.
6. Select "MCP: Azure MCP Server" from the dropdown menu.
7. Click on the "Set Log Level..." icon and choose "Trace" or "Debug".

### Collecting logs with PerfView

[PerfView](https://github.com/Microsoft/perfview) is a free, performance-analysis tool that runs on Windows. To collect traces:

1. Download and open [PerfView](https://github.com/Microsoft/perfview).
2. Select the "Collect" file menu item then "Collect".
3. Find the process ID for the server, azmcp.exe.
4. Select the "Focus process" checkbox. Enter the process ID or executable name, azmcp.exe in the text box.
5. Expand the "Advanced Options" section.
6. In the "Additional Providers" list, add `*Microsoft-Extensions-Logging` to the list. This includes the `*`.
7. Press "Start Collection".

### Visualizing EventSource logs in PerfView

1. Download and open [PerfView](https://github.com/Microsoft/perfview).
2. On the left side, in the file explorer, double-click to expand the `.nettrace` file.
3. Select the "Events" item.
4. Under the Event Types, examine the events under `Microsoft-Extensions-Logging/*`

## Authentication

### 401 Unauthorized: Local authorization is disbaled.

This error indicates that the targeted resource is configured to disallow access using **Access Keys**, which are currently used by Azure MCP for authentication in certain scenarios.

#### Root Cause

Azure MCP currently relies on **access key-based authentication** for some resources. However, many Azure services (e.g., **Cosmos DB**, **Azure Storage**) can be configured to enforce **Azure Entra ID** (formerly AAD) authentication only, thereby disabling local authorization mechanisms such as:

- Primary or secondary access keys
- Shared access signatures (SAS)
- Connection strings containing embedded keys

When these local authorization methods are disabled, any access attempt from Azure MCP using them will result in a `401 Unauthorized` error.

#### Upcoming Enhancement

Support for **Azure Entra ID-based authentication** in these scenarios is to be added in an upcoming release of Azure MCP. This will allow the MCP server to authenticate using federated identity or managed identity flows.

> ℹ️ **Until Entra ID support is available**, ensure that local authorization is enabled for the target resource being accessed by Azure MCP. The latest status can be tracked in this [issue](https://github.com/Azure/azure-mcp/issues/27)


### AADSTS500200 error: User account is a personal Microsoft account

This error occurs because the Azure MCP server uses Azure Identity SDK's `DefaultAzureCredential` for authentication, which is specifically designed for Azure Active Directory (Azure Entra ID) authentication flows, as they're designed to work with Azure services that require Azure AD-based authentication and authorization. See the [Authentication](https://github.com/Azure/blob/main/README.md#-authentication) section in for more details.

Personal Microsoft accounts (@hotmail.com, @outlook.com, or @live.com) use a different authentication system that isn't compatible with these flows.

To resolve this issue, you can:
- Use a work or school account that's part of an Azure AD tenant.
- Request access to an Azure subscription with your existing organizational account.
    - Learn more: [Add organization users and manage access](https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/add-organization-users?view=azure-devops&tabs=browser).
- Create a new Azure subscription and associated Azure AD tenant.
    - Learn more: [Associate or add an Azure subscription to your Microsoft Entra tenant](https://learn.microsoft.com/en-us/entra/fundamentals/how-subscriptions-associated-directory).
- If you must use a personal account, first create an Azure AD tenant for your Azure subscription, then authenticate using that tenant.
    - Learn more: [Quickstart: Create a new tenant in Microsoft Entra ID](https://learn.microsoft.com/en-us/entra/fundamentals/create-new-tenant), [Set up a new Microsoft Entra tenant](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-create-new-tenant).

## Common issues

### Console window is empty when running Azure MCP Server

By default, Azure MCP Server communicates with MCP Clients via standard I/O. Any logs output to standard I/O are subject to interpretation from the MCP Client. See [Logging](#logging) on how to view logs.

