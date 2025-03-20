using System.CommandLine;
using AzureMCP.Models;
using AzureMCP.Services.Interfaces;
using AzureMCP.Arguments.Cosmos;
using System.CommandLine.Parsing;

namespace AzureMCP.Commands.Cosmos;

public class ContainersListCommand : BaseCosmosCommand<ContainersListArguments>
{
    public ContainersListCommand() : base()
    {
        RegisterArgumentChain(
            CreateAccountArgument(),
            CreateDatabaseArgument()
        );
    }

    public override Command GetCommand()
    {
        var command = new Command(
            "list", 
            "List all containers in a Cosmos DB database. This command retrieves and displays all containers within the specified database and Cosmos DB account. Results include container names and are returned as a JSON array. You must specify both an account name and a database name.");
            
        AddBaseOptionsToCommand(command);
        command.AddOption(_accountOption);
        command.AddOption(_databaseOption);
        return command;
    }

    protected override ContainersListArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Account = parseResult.GetValueForOption(_accountOption);
        args.Database = parseResult.GetValueForOption(_databaseOption);
        return args;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindArguments(parseResult);

        try
        {
            if (!await ProcessArgumentChain(context, options))
            {
                return context.Response;
            }

            var cosmosService = context.GetService<ICosmosService>();
            var containers = await cosmosService.ListContainers(
                options.Account!, 
                options.Database!, 
                options.SubscriptionId!, 
                options.TenantId,
                options.RetryPolicy);
                
            context.Response.Results = containers?.Count > 0 ? 
                new { containers } : 
                null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}