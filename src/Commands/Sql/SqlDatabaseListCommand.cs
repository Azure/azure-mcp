using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Sql;
using AzureMcp.Extensions;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureMcp.Commands.Sql;

public sealed class SqlDatabaseListCommand() : BaseCommand
{
    private static readonly Option<string> SubscriptionOption = ArgumentDefinitions.Common.Subscription.ToOption();
    private static readonly Option<string> ServerNameOption = ArgumentDefinitions.Sql.Server.ToOption();

    protected override string GetCommandName() => "list";
    protected override string GetCommandDescription() => "List all SQL Databases for a given server";

    protected override void RegisterOptions(Command command)
    {
        command.AddOption(SubscriptionOption);
        command.AddOption(ServerNameOption);
    }

    private static SqlDatabaseListArguments BindArguments(ParseResult parseResult)
    {
        return new SqlDatabaseListArguments(
            parseResult.GetValueForOption(SubscriptionOption),
            parseResult.GetValueForOption(ServerNameOption)
        );
    }

    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var args = BindArguments(commandOptions);
        var databases = GetSqlDatabases(args.Subscription, args.ServerName);
        var json = JsonSerializer.Serialize(databases, new JsonSerializerOptions { WriteIndented = true });
        var response = new CommandResponse
        {
            Status = 0,
            Message = "SQL Databases listed successfully.",
            Results = json
        };
        return Task.FromResult(response);
    }

    private static List<object> GetSqlDatabases(string? subscription, string? serverName)
    {
        var credential = new DefaultAzureCredential();
        var armClient = new ArmClient(credential);
        var result = new List<object>();
        if (string.IsNullOrEmpty(subscription) || string.IsNullOrEmpty(serverName))
            return result;
        var subscriptionResource = armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(subscription));
        foreach (var sqlServer in subscriptionResource.GetSqlServers())
        {
            if (!sqlServer.Data.Name.Equals(serverName, StringComparison.OrdinalIgnoreCase))
                continue;
            foreach (var db in sqlServer.GetSqlDatabases())
            {
                result.Add(new
                {
                    Name = db.Data.Name,
                    Status = db.Data.Status.ToString(),
                    Collation = db.Data.Collation,
                    Sku = db.Data.Sku?.Name,
                    MaxSizeBytes = db.Data.MaxSizeBytes,
                    Server = serverName,
                    Subscription = subscription
                });
            }
        }
        return result;
    }
}