// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Cosmos;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Azure.Cosmos;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseCosmosCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : SubscriptionCommand<TArgs> where TArgs : BaseCosmosArguments, new()
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
            .WithIsRequired(ArgumentDefinitions.Cosmos.Account.Required);
}
