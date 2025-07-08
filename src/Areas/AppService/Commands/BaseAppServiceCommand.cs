// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.AppService.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
namespace AzureMcp.Areas.AppService.Commands;

public abstract class BaseAppServiceCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : BaseAppServiceOptions, new()
{
    protected readonly Option<string> _appNameOption = AppServiceOptionDefinitions.AppName;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_appNameOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.AppName = parseResult.GetValueForOption(_appNameOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return options;
    }
}
