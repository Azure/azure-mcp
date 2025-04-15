# Troubleshooting

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

## Common issues

### Console window is empty when running Azure MCP Server

By default, Azure MCP Server communicates with MCP Clients via standard I/O. Any logs output to standard I/O are subject to interpretation from the MCP Client. See [Logging](#logging) on how to view logs.