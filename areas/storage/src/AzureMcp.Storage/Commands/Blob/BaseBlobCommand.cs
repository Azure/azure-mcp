// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Storage.Commands.Blob.Container;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.Blob;

namespace AzureMcp.Storage.Commands.Blob;

public abstract class BaseBlobCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseContainerCommand<TOptions> where TOptions : BaseBlobOptions, new()
{
    protected readonly Option<string> _blobOption = StorageOptionDefinitions.Blob;

    protected BaseBlobCommand()
    {
    }

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_blobOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Blob = parseResult.GetValueForOption(_blobOption);
        return options;
    }
}
