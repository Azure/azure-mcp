// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Models.Option;
using AzureMcp.Options.Cosmos;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseCosmosCommand<TOptions> where TOptions : BaseDatabaseOptions, new()
{
    protected readonly Option<string> _databaseOption = OptionDefinitions.Cosmos.Database;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Database = parseResult.GetValueForOption(_databaseOption);
        return options;
    }
}
