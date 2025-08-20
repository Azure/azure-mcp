// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.Queue;

namespace AzureMcp.Storage.Commands.Queue;

public abstract class BaseQueueCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : BaseStorageCommand<T>
    where T : BaseQueueOptions, new()
{
    protected readonly Option<string> _queueOption = StorageOptionDefinitions.Queue;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_queueOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Queue = parseResult.GetValueForOption(_queueOption);
        return options;
    }
}
