// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Storage.Commands;
using AzureMcp.Areas.Storage.Options;
using AzureMcp.Areas.Storage.Options.DataLake;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Storage.Commands.DataLake.FileSystem;

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
