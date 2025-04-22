using AzureMcp.Models.Command;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Server;

public sealed class SqlServerListCommand() : BaseCommand
{
    protected override string GetCommandName() => "list";
    protected override string GetCommandDescription() => "List all SQL Servers";

    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var servers = GetSqlServers();
        var json = JsonSerializer.Serialize(servers, new JsonSerializerOptions { WriteIndented = true });
        var response = new CommandResponse
        {
            Status = 0,
            Message = "SQL Servers listed successfully.",
            Results = json
        };
        return Task.FromResult(response);
    }

    private static object GetSqlServers()
    {
        return new[]
        {
            new { Name = "sqlserver1", Location = "eastus" },
            new { Name = "sqlserver2", Location = "westus2" }
        };
    }
}