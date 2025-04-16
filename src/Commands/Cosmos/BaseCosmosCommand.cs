// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Cosmos;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Azure.Cosmos;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseCosmosCommand<TArgs> : SubscriptionCommand<TArgs> where TArgs : BaseCosmosArguments, new()
{
    protected readonly Option<string> _accountOption = ArgumentDefinitions.Cosmos.Account.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_accountOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateAccountArgument());
    }

    // Common method to get account options
    protected async Task<List<ArgumentOption>> GetAccountOptions(CommandContext context, string subscription)
    {
        if (string.IsNullOrEmpty(subscription)) return [];

        var cosmosService = context.GetService<ICosmosService>();
        var accounts = await cosmosService.GetCosmosAccounts(subscription);

        return accounts?.Select(a => new ArgumentOption { Name = a, Id = a }).ToList() ?? [];
    }

    // Common method to get database options
    protected async Task<List<ArgumentOption>> GetDatabaseOptions(CommandContext context, string accountName, string subscription)
    {
        if (string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(subscription))
            return [];

        var cosmosService = context.GetService<ICosmosService>();
        var databases = await cosmosService.ListDatabases(accountName, subscription);

        return databases?.Select(d => new ArgumentOption { Name = d, Id = d }).ToList() ?? [];
    }

    // Common method to get container options
    protected async Task<List<ArgumentOption>> GetContainerOptions(CommandContext context, string accountName, string databaseName, string subscription)
    {
        if (string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(subscription))
            return [];

        var cosmosService = context.GetService<ICosmosService>();
        var containers = await cosmosService.ListContainers(accountName, databaseName, subscription);

        return containers?.Select(c => new ArgumentOption { Name = c, Id = c }).ToList() ?? [];
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        CosmosException cosmosEx => cosmosEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        CosmosException cosmosEx => (int)cosmosEx.StatusCode,
        _ => base.GetStatusCode(ex)
    };

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Account = parseResult.GetValueForOption(_accountOption);
        return args;
    }

    // Helper methods for creating Cosmos-specific arguments
    protected ArgumentBuilder<TArgs> CreateAccountArgument() =>
        ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Cosmos.Account.Name, ArgumentDefinitions.Cosmos.Account.Description)
            .WithValueAccessor(args => args.Account ?? string.Empty)
            .WithSuggestedValuesLoader(async (context, args) => await GetAccountOptions(context, args.Subscription ?? string.Empty))
            .WithIsRequired(ArgumentDefinitions.Cosmos.Account.Required);
}