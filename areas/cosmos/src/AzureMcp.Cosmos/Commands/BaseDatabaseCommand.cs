// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Cosmos.Options;

namespace AzureMcp.Cosmos.Commands;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseCosmosCommand<TOptions> where TOptions : BaseDatabaseOptions, new()
{
    protected readonly Option<string> _databaseOption = CosmosOptionDefinitions.Database;

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
