using AzureMcp.Models.Command;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql;
using Azure.ResourceManager.Resources;
using Microsoft.Data.SqlClient;
using AzureMcp.Models.Argument;
using AzureMcp.Extensions;

namespace AzureMcp.Commands.Sql;

public sealed class SqlDatabaseQueryCommand(ISqlDatabaseQueryService queryService) : BaseCommand
{
    private static readonly Option<string> SubscriptionOption = ArgumentDefinitions.Common.Subscription.ToOption();
    private static readonly Option<string> ServerNameOption = ArgumentDefinitions.Sql.Server.ToOption();
    private static readonly Option<string> DatabaseNameOption = ArgumentDefinitions.Sql.Database.ToOption();
    private static readonly Option<string> QueryOption = ArgumentDefinitions.Sql.Query.ToOption();

    protected override string GetCommandName() => "query";
    protected override string GetCommandDescription() => "Query a SQL Database using a SQL statement.";

    protected override void RegisterOptions(Command command)
    {
        command.AddOption(SubscriptionOption);
        command.AddOption(ServerNameOption);
        command.AddOption(DatabaseNameOption);
        command.AddOption(QueryOption);
    }

    private static SqlDatabaseQueryArguments BindArguments(ParseResult parseResult)
    {
        return new SqlDatabaseQueryArguments(
            parseResult.GetValueForOption(SubscriptionOption),
            parseResult.GetValueForOption(ServerNameOption),
            parseResult.GetValueForOption(DatabaseNameOption),
            parseResult.GetValueForOption(QueryOption)
        );
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var args = BindArguments(commandOptions);
        var results = await queryService.ExecuteQueryAsync(args.Subscription!, args.ServerName!, args.DatabaseName!, args.Query!);
        var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        var response = new CommandResponse
        {
            Status = 0,
            Message = "SQL query executed successfully.",
            Results = json
        };
        return response;
    }
}
