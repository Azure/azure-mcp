// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options;
using AzureMcp.Options.Monitor.ApplicationInsights;

namespace AzureMcp.Commands.Monitor.ApplicationInsights;

public abstract class BaseApplicationInsightsCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, IApplicationInsightsOptions, new()
{
    protected readonly Option<string> _appIdOption = OptionDefinitions.Monitor.AppId;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_appIdOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        
        // Cast to the right interface that has the AppId property
        if (options is AppTraceOptions traceOptions)
        {
            traceOptions.AppId = parseResult.GetValueForOption(_appIdOption);
        }
        else if (options is AppDiagnoseOptions diagnoseOptions)
        {
            diagnoseOptions.AppId = parseResult.GetValueForOption(_appIdOption);
        }
        
        return options;
    }
}
