// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.Share.File;

namespace AzureMcp.Storage.Commands.Share.File;

public abstract class BaseFileCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseShareCommand<TOptions> where TOptions : BaseFileOptions, new()
{
    protected readonly Option<string> _directoryPathOption = StorageOptionDefinitions.DirectoryPath;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_directoryPathOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.DirectoryPath = parseResult.GetValueForOption(_directoryPathOption);
        return options;
    }
}
