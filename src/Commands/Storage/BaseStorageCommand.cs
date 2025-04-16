// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Storage;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;

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
            .WithSuggestedValuesLoader(async (context, args) =>
            {
                if (string.IsNullOrEmpty(args.Subscription)) return [];

                var storageService = context.GetService<IStorageService>();
                var accounts = await storageService.GetStorageAccounts(args.Subscription);

                return accounts?.Select(a => new ArgumentOption { Name = a, Id = a }).ToList() ?? [];
            })
            .WithIsRequired(ArgumentDefinitions.Storage.Account.Required);
    }
}