// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Sql.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Sql.Commands;

public abstract class BaseSqlCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseSqlCommand<TOptions>> logger)
    : SubscriptionCommand<TOptions> where TOptions : BaseSqlOptions, new()
{
    protected readonly Option<string> _serverOption = SqlOptionDefinitions.Server;
    protected readonly ILogger<BaseSqlCommand<TOptions>> _logger = logger;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
        command.AddOption(_serverOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        options.Server = parseResult.GetValueForOption(_serverOption);
        return options;
    }
}
