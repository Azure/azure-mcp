// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Azure.Core;
using AzureMcp.Helpers;
using AzureMcp.Models.Option;
using AzureMcp.Options;
using AzureMcp.Services.Telemetry;

namespace AzureMcp.Commands.Subscription;

public abstract class SubscriptionCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions> : GlobalCommand<TOptions>
    where TOptions : SubscriptionOptions, new()
{

    protected readonly Option<string> _subscriptionOption = OptionDefinitions.Common.Subscription;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_subscriptionOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Subscription = parseResult.GetValueForOption(_subscriptionOption);
        return options;
    }

    protected void AddSubscriptionInformation(Activity? activity, TOptions options)
    {
        activity?.AddTag(TelemetryConstants.TagName.SubscriptionGuid, options.Subscription);
    }

    protected void AddResourceInformation(Activity? activity, params string[] parts)
    {
        if (activity is null || parts.Length == 0)
        {
            return;
        }

        var constructed = string.Join('/', parts);
        var hashedString = Sha256Helper.GetHashedValue(constructed);
        activity.AddTag(TelemetryConstants.TagName.ResourceHash, hashedString);
    }

    protected void AddResourceInformation(Activity? activity, ResourceIdentifier? resourceIdentifier)
    {
        if (activity is null || resourceIdentifier is null)
        {
            return;
        }

        AddResourceInformation(activity, resourceIdentifier.ToString());
    }

    protected string GetResourceUri(string subscriptionId, string resourceGroup, string resourceProviderGroup, params string[] resources)
    {
        var remaining = string.Join('/', resources);

        return $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/{resourceProviderGroup}/{remaining}";
    }

}
