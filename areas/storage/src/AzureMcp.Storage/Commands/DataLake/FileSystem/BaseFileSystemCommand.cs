// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Storage.Commands;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.DataLake;

namespace AzureMcp.Storage.Commands.DataLake.FileSystem;

public abstract class BaseFileSystemCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseStorageCommand<TOptions> where TOptions : BaseFileSystemOptions, new()
{
    protected readonly Option<string> _fileSystemOption = StorageOptionDefinitions.FileSystem;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_fileSystemOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.FileSystem = parseResult.GetValueForOption(_fileSystemOption);
        return options;
    }
}
