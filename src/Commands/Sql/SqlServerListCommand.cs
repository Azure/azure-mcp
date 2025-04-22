using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql;
using AzureMcp.Models.Command;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Sql;

public sealed class SqlServerListCommand(ISqlServerLister sqlServerLister) : BaseCommand
{
    protected override string GetCommandName() => "list";
    protected override string GetCommandDescription() => "List all SQL Servers";

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var servers = await sqlServerLister.ListSqlServersAsync();
        var json = JsonSerializer.Serialize(servers, new JsonSerializerOptions { WriteIndented = true });
        var response = new CommandResponse
        {
            Status = 0,
            Message = "SQL Servers listed successfully.",
            Results = json
        };
        return response;
    }

    public static string GetResourceGroupName(string resourceId)
    {
        var parts = resourceId.Split('/');
        for (int i = 0; i < parts.Length - 1; i++)
        {
            if (parts[i].Equals("resourceGroups", System.StringComparison.OrdinalIgnoreCase) && i + 1 < parts.Length)
                return parts[i + 1];
        }
        return string.Empty;
    }

    public static async Task<IReadOnlyList<object>> ListSqlServersAsync()
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