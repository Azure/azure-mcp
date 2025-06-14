// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Models.Option;
using AzureMcp.Options.Sql.Server;

namespace AzureMcp.Commands.Sql.Server;

public abstract class BaseServerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseSqlCommand<TOptions> where TOptions : BaseServerOptions, new()
{
    private readonly Option<string> _serverOption = OptionDefinitions.Sql.Server;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serverOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ServerName = parseResult.GetValueForOption(_serverOption);
        return options;
    }
}
