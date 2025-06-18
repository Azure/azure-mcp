// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.AppConfig.Commands;
using AzureMcp.Areas.AppConfig.Options.KeyValue;
using AzureMcp.Commands;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.AppConfig.Commands.KeyValue;

public abstract class BaseKeyValueCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : BaseAppConfigCommand<T> where T : BaseKeyValueOptions, new()
{
    protected readonly Option<string> _keyOption = OptionDefinitions.AppConfig.Key;
    protected readonly Option<string> _labelOption = OptionDefinitions.AppConfig.Label;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_keyOption);
        command.AddOption(_labelOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Key = parseResult.GetValueForOption(_keyOption);
        options.Label = parseResult.GetValueForOption(_labelOption);
        return options;
    }
}
