// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMCP.Arguments.AppConfig.KeyValue;
using AzureMCP.Models.Command;
using AzureMCP.Services.Interfaces;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMCP.Commands.AppConfig.KeyValue;

public sealed class KeyValueDeleteCommand : BaseKeyValueCommand<KeyValueDeleteArguments>
{
    protected override string GetCommandName() => "delete";

    protected override string GetCommandDescription() =>
        """
        Delete a key-value pair from an App Configuration store. This command removes the specified key-value pair from the store. 
        If a label is specified, only the labeled version is deleted. If no label is specified, the key-value with the matching 
        key and the default label will be deleted.
        """;

    [McpServerTool(Destructive = true, ReadOnly = false)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var appConfigService = context.GetService<IAppConfigService>();
            await appConfigService.DeleteKeyValue(
                args.Account!,
                args.Key!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy,
                args.Label);

            context.Response.Results = new { key = args.Key, label = args.Label };
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}