// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public abstract class BaseKeyValueCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : BaseAppConfigCommand<T> where T : BaseKeyValueArguments, new()
{
    protected readonly Option<string> _keyOption = ArgumentDefinitions.AppConfig.Key;
    protected readonly Option<string> _labelOption = ArgumentDefinitions.AppConfig.Label;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_keyOption);
        command.AddOption(_labelOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Key = parseResult.GetValueForOption(_keyOption);
        args.Label = parseResult.GetValueForOption(_labelOption);
        return args;
    }
}
