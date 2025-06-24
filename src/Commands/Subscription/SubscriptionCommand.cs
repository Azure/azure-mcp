// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using Azure.Core;
using AzureMcp.Models.Option;
using AzureMcp.Options;
using AzureMcp.Services.Telemetry;

namespace AzureMcp.Commands.Subscription;

public abstract class SubscriptionCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions> : GlobalCommand<TOptions>
    where TOptions : SubscriptionOptions, new()
{
    private static readonly SHA256 s_sHA256 = SHA256.Create();
    private static readonly Encoding s_encoding = Encoding.UTF8;

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
            var resourceHash = GetHashedValue(resourceId);
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

        var hashedString = GetHashedValue(resourceIdentifier.ToString());
        activity.AddTag(TelemetryConstants.TagName.ResourceHash, hashedString);
    }

    protected string GetResourceUri(string subscriptionId, string resourceGroup, string provider, string[] components)
    {
        var remaining = string.Join('/', components);

        return $"resource/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/{provider}/{remaining}";
    }

    private static string GetHashedValue(string contents)
    {
        var bytes = s_sHA256.ComputeHash(s_encoding.GetBytes(contents));
        return string.Join(string.Empty, bytes.Select(x => x.ToString("x2")));
    }
}
