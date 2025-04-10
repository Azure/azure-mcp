using AzureMCP.Arguments.Cosmos;
using AzureMCP.Models.Argument;
using AzureMCP.Models.Command;
using AzureMCP.Services.Interfaces;
using ModelContextProtocol.Server;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMCP.Commands.Cosmos;

public sealed class ItemQueryCommand : BaseDatabaseCommand<ItemQueryArguments>
{
    private readonly Option<string> _queryOption = ArgumentDefinitions.Cosmos.Query.ToOption();
    private readonly Option<string> _containerOption = ArgumentDefinitions.Cosmos.Container.ToOption();

    public ItemQueryCommand() : base()
    {
        RegisterArguments();
    }

    protected override string GetCommandName() => "query";

    protected override string GetCommandDescription() =>
        $"""
        Execute a SQL query against items in a Cosmos DB container. Requires {ArgumentDefinitions.Cosmos.AccountName}, 
        {ArgumentDefinitions.Cosmos.DatabaseName}, and {ArgumentDefinitions.Cosmos.ContainerName}. 
        The {ArgumentDefinitions.Cosmos.QueryText} parameter accepts SQL query syntax. Results are returned as a 
        JSON array of documents.
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_containerOption);
        command.AddOption(_queryOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateContainerArgument());
        AddArgument(CreateQueryArgument());
    }

    private ArgumentBuilder<ItemQueryArguments> CreateContainerArgument()
    {
        return ArgumentBuilder<ItemQueryArguments>
            .Create(ArgumentDefinitions.Cosmos.Container.Name, ArgumentDefinitions.Cosmos.Container.Description)
            .WithValueAccessor(args => args.Container ?? string.Empty)
            .WithSuggestedValuesLoader(async (context, args) =>
                await GetContainerOptions(
                    context,
                    args.Account ?? string.Empty,
                    args.Database ?? string.Empty,
                    args.Subscription ?? string.Empty))
            .WithIsRequired(ArgumentDefinitions.Cosmos.Container.Required);
    }

    private static ArgumentBuilder<ItemQueryArguments> CreateQueryArgument()
    {
        var defaultValue = ArgumentDefinitions.Cosmos.Query.DefaultValue ?? "SELECT * FROM c";
        return ArgumentBuilder<ItemQueryArguments>
            .Create(ArgumentDefinitions.Cosmos.Query.Name, ArgumentDefinitions.Cosmos.Query.Description)
            .WithValueAccessor(args => args.Query ?? string.Empty)
            .WithDefaultValue(defaultValue)
            .WithIsRequired(ArgumentDefinitions.Cosmos.Query.Required);
    }

    protected override ItemQueryArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Query = parseResult.GetValueForOption(_queryOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var cosmosService = context.GetService<ICosmosService>();
            var items = await cosmosService.QueryItems(
                args.Account!,
                args.Database!,
                args.Container!,
                args.Query ?? "SELECT * FROM c",
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = items?.Count > 0 ?
                new { items } :
                null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}