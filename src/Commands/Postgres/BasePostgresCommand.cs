// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Postgres;
using AzureMcp.Models.Argument;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres;

public abstract class BasePostgresCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : SubscriptionCommand<TArgs> where TArgs : BasePostgresArguments, new()
{
    protected readonly Option<string> _userOption = ArgumentDefinitions.Postgres.User;

    protected readonly ILogger<BasePostgresCommand<TArgs>> _logger;

    protected BasePostgresCommand(ILogger<BasePostgresCommand<TArgs>> logger)
    {
        _logger = logger;
    }

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
        command.AddOption(_userOption);
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        args.User = parseResult.GetValueForOption(_userOption);
        return args;
    }
}
