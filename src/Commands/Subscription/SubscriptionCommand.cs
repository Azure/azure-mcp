// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
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
        activity?.AddTag(TelemetryConstants.SubscriptionGuid, options.Subscription);
    }

    protected void AddResourceInformation(Activity? activity, string? resourceId)
    {
        if (string.IsNullOrEmpty(resourceId))
        {
            return;
        }

        var bytes = s_sHA256.ComputeHash(s_encoding.GetBytes(resourceId));
        var hashedString = string.Join(string.Empty, bytes.Select(x => x.ToString("x2")));

        activity?.AddTag(TelemetryConstants.ResourceHash, hashedString);
    }
}
