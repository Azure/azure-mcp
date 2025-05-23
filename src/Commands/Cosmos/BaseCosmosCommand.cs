// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Cosmos;
using AzureMcp.Models.Argument;
using Microsoft.Azure.Cosmos;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseCosmosCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : SubscriptionCommand<TArgs> where TArgs : BaseCosmosArguments, new()
{
    protected readonly Option<string> _accountOption = ArgumentDefinitions.Cosmos.Account;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_accountOption);
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Account = parseResult.GetValueForOption(_accountOption);
        return args;
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
}
