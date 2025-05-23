// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Storage.Blob;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Storage.Blob.Container;

public abstract class BaseContainerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseStorageCommand<TArgs> where TArgs : BaseContainerArguments, new()
{
    protected readonly Option<string> _containerOption = ArgumentDefinitions.Storage.Container;

    protected BaseContainerCommand()
    {
    }

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_containerOption);
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Container = parseResult.GetValueForOption(_containerOption);
        return args;
    }
}
