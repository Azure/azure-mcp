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

    protected void AddResourceInformation(Activity? activity, string? resourceId)
    {
        if (activity is null || string.IsNullOrEmpty(resourceId))
        {
            return;
        }

        if (ResourceIdentifier.TryParse(resourceId, out var resource))
        {
            AddResourceInformation(activity, resource);
        }
        else
        {
            var resourceHash = Sha256Helper.GetHashedValue(resourceId);
            activity.AddTag(TelemetryConstants.TagName.ResourceHash, resourceHash)
                .AddTag(TelemetryConstants.TagName.IsCalculated, true);
        }
    }

    protected void AddResourceInformation(Activity? activity, ResourceIdentifier? resourceIdentifier)
    {
        if (activity is null || resourceIdentifier is null)
        {
            return;
        }

        var hashedString = Sha256Helper.GetHashedValue(resourceIdentifier.ToString());
        activity.AddTag(TelemetryConstants.TagName.ResourceHash, hashedString);
    }

    protected string GetResourceUri(string subscriptionId, string resourceGroup, string resourceProviderGroup, params string[] resources)
    {
        var remaining = string.Join('/', resources);

        return $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/{resourceProviderGroup}/{remaining}";
    }

}
