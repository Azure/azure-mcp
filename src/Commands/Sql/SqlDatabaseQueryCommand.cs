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

namespace AzureMcp.Commands.Sql;

public sealed class SqlDatabaseQueryCommand() : BaseCommand
{
    public static Option<string> SubscriptionOption { get; } = new Option<string>(
        name: "--subscription",
        description: "Azure subscription ID or name.",
        parseArgument: result => result.Tokens.Count > 0 ? result.Tokens[0].Value : null
    );
    public static Option<string> ServerNameOption { get; } = new Option<string>(
        name: "--server-name",
        description: "SQL Server name.",
        parseArgument: result => result.Tokens.Count > 0 ? result.Tokens[0].Value : null
    );
    public static Option<string> DatabaseNameOption { get; } = new Option<string>(
        name: "--database-name",
        description: "SQL Database name.",
        parseArgument: result => result.Tokens.Count > 0 ? result.Tokens[0].Value : null
    );
    public static Option<string> QueryOption { get; } = new Option<string>(
        name: "--query",
        description: "SQL query to execute.",
        parseArgument: result => result.Tokens.Count > 0 ? result.Tokens[0].Value : null
    );

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
        var results = await ExecuteSqlQueryAsync(args);
        var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        var response = new CommandResponse
        {
            Status = 0,
            Message = "SQL query executed successfully.",
            Results = json
        };
        return response;
    }

    private static async Task<object> ExecuteSqlQueryAsync(SqlDatabaseQueryArguments args)
    {
        // Discover the server FQDN from Azure ResourceManager
        var credential = new DefaultAzureCredential();
        var armClient = new ArmClient(credential);
        var subscriptionResource = armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(args.Subscription!));
        string? fqdn = null;
        foreach (var sqlServer in subscriptionResource.GetSqlServers())
        {
            if (sqlServer.Data.Name.Equals(args.ServerName, StringComparison.OrdinalIgnoreCase))
            {
                fqdn = sqlServer.Data.FullyQualifiedDomainName;
                break;
            }
        }
        if (string.IsNullOrEmpty(fqdn))
            return new { Error = "SQL Server not found." };

        // Build connection string (assumes Azure AD integrated auth)
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = fqdn,
            InitialCatalog = args.DatabaseName,
            Authentication = SqlAuthenticationMethod.ActiveDirectoryDefault
        };
        using var conn = new SqlConnection(builder.ConnectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(args.Query, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        var rows = new List<Dictionary<string, object?>>();
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>();
            for (int i = 0; i < reader.FieldCount; i++)
                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
            rows.Add(row);
        }
        return rows;
    }
}
