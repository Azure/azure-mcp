// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.MySql.Options;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands;

public abstract class BaseMySqlCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseMySqlCommand<TOptions>> logger)
    : SubscriptionCommand<TOptions> where TOptions : BaseMySqlOptions, new()
{
    protected readonly Option<string> _userOption = MySqlOptionDefinitions.User;

    protected readonly ILogger<BaseMySqlCommand<TOptions>> _logger = logger;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        RequireResourceGroup();
        command.AddOption(_userOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.User = parseResult.GetValueForOption(_userOption);
        return options;
    }
}
