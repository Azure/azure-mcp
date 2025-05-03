// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Storage;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Storage;

public abstract class BaseStorageCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T>
    where T : BaseStorageArguments, new()
{
    protected readonly Option<string> _accountOption = ArgumentDefinitions.Storage.Account.ToOption();

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

    protected override T BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Account = parseResult.GetValueForOption(_accountOption);
        return args;
    }

    protected ArgumentBuilder<T> CreateAccountArgument()
    {
        return ArgumentBuilder<T>
            .Create(ArgumentDefinitions.Storage.Account.Name, ArgumentDefinitions.Storage.Account.Description)
            .WithValueAccessor(args => args.Account ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Storage.Account.Required);
    }
}
