using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql;
using AzureMcp.Models.Command;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Sql;

public sealed class ServerListCommand() : BaseCommand
{
    protected override string GetCommandName() => "list";
    protected override string GetCommandDescription() => "List all SQL Servers";

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var servers = await GetSqlServersAsync();
        var json = JsonSerializer.Serialize(servers, new JsonSerializerOptions { WriteIndented = true });
        var response = new CommandResponse
        {
            Status = 0,
            Message = "SQL Servers listed successfully.",
            Results = json
        };
        return response;
    }

    private static string GetResourceGroupName(string resourceId)
    {
        // Resource ID format: /subscriptions/{sub}/resourceGroups/{rg}/providers/Microsoft.Sql/servers/{name}
        var parts = resourceId.Split('/');
        for (int i = 0; i < parts.Length - 1; i++)
        {
            if (parts[i].Equals("resourceGroups", System.StringComparison.OrdinalIgnoreCase) && i + 1 < parts.Length)
                return parts[i + 1];
        }
        return string.Empty;
    }

    private static async Task<List<object>> GetSqlServersAsync()
    {
        var credential = new DefaultAzureCredential();
        var armClient = new ArmClient(credential);
        var result = new List<object>();
        await foreach (var sub in armClient.GetSubscriptions().GetAllAsync())
        {
            var sqlServers = sub.GetSqlServers();
            foreach (var server in sqlServers)
            {
                result.Add(new
                {
                    Name = server.Data.Name,
                    Location = server.Data.Location.ToString(),
                    ResourceGroup = GetResourceGroupName(server.Id.ToString()),
                    FullyQualifiedDomainName = server.Data.FullyQualifiedDomainName
                });
            }
        }
        return result;
    }
}