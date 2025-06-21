using AzureMcp.Areas.Server.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Server;

internal sealed class ServerSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // No additional services needed for Server area
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create MCP Server command group
        var mcpServer = new CommandGroup("server", "MCP Server operations - Commands for managing and interacting with the MCP Server.");
        rootGroup.AddSubGroup(mcpServer);

        // Register MCP Server commands
        var startServer = new ServiceStartCommand();
        mcpServer.AddCommand("start", startServer);
    }
}
