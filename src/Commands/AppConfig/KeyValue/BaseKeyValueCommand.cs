// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.AppConfig.KeyValue;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.AppConfig.KeyValue;

public abstract class BaseKeyValueCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : BaseAppConfigCommand<T> where T : BaseKeyValueArguments, new()
{
    protected readonly Option<string> _keyOption = ArgumentDefinitions.AppConfig.Key.ToOption();
    protected readonly Option<string> _labelOption = ArgumentDefinitions.AppConfig.Label.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_keyOption);
        command.AddOption(_labelOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateKeyArgument());
        AddArgument(CreateLabelArgument());
    }

    protected override T BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Key = parseResult.GetValueForOption(_keyOption);
        args.Label = parseResult.GetValueForOption(_labelOption);
        return args;
    }

    protected virtual ArgumentBuilder<T> CreateKeyArgument() =>
        ArgumentBuilder<T>
            .Create(ArgumentDefinitions.AppConfig.Key.Name, ArgumentDefinitions.AppConfig.Key.Description)
            .WithValueAccessor(args => args.Key ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.AppConfig.Key.Required);

    protected virtual ArgumentBuilder<T> CreateLabelArgument() =>
        ArgumentBuilder<T>
            .Create(ArgumentDefinitions.AppConfig.Label.Name, ArgumentDefinitions.AppConfig.Label.Description)
            .WithValueAccessor(args => args.Label ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.AppConfig.Label.Required);
}
