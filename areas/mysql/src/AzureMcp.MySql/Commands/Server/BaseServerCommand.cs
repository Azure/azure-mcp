// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.MySql.Options;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands.Server;

public abstract class BaseServerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseMySqlCommand<TOptions>> logger)
    : BaseMySqlCommand<TOptions>(logger) where TOptions : MySqlServerOptions, new()
{
    private readonly Option<string> _serverOption = MySqlOptionDefinitions.Server;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serverOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Server = parseResult.GetValueForOption(_serverOption);
        return options;
    }
}
