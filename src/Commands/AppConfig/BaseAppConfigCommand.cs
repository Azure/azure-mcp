// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.AppConfig;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;

namespace AzureMcp.Commands.AppConfig;

public abstract class BaseAppConfigCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseAppConfigArguments, new()
{
    protected readonly Option<string> _accountOption = ArgumentDefinitions.AppConfig.Account.ToOption();

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

    protected ArgumentBuilder<T> CreateAccountArgument() =>
        ArgumentBuilder<T>
            .Create(ArgumentDefinitions.AppConfig.Account.Name, ArgumentDefinitions.AppConfig.Account.Description)
            .WithValueAccessor(args => args.Account ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.AppConfig.Account.Required);
}