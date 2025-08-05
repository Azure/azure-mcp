// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.MySql.Options;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands;

public abstract class BaseServerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseMySqlCommand<TOptions>> logger)
    : BaseMySqlCommand<TOptions>(logger) where TOptions : BaseMySqlOptions, new()

{
    private readonly Option<string> _serverOption = MySqlOptionDefinitions.Server;

    public override string Name => "server";

    public override string Description =>
        "Retrieves information about a MySQL server.";

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