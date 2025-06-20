// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Models.Option;
using AzureMcp.Options.AppConfig.FeatureFlag;

namespace AzureMcp.Commands.AppConfig.FeatureFlag;

public abstract class BaseFeatureFlagCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : BaseAppConfigCommand<T> where T : BaseFeatureFlagOptions, new()
{
    protected readonly Option<string> _featureFlagNameOption = OptionDefinitions.AppConfig.FeatureFlag.FeatureFlagName;
    protected readonly Option<string> _labelOption = OptionDefinitions.AppConfig.Label;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_featureFlagNameOption);
        command.AddOption(_labelOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.FeatureFlagName = parseResult.GetValueForOption(_featureFlagNameOption);
        options.Label = parseResult.GetValueForOption(_labelOption);
        return options;
    }
}
