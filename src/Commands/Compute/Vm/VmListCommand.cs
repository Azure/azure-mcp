// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Compute.Vm;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Compute.Vm;

public sealed class VmListCommand(ILogger<VmListCommand> logger) : SubscriptionCommand<VmListArguments>
{
    private readonly ILogger<VmListCommand> _logger = logger;

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        """
        List all virtual machines in a subscription. This command retrieves and displays all virtual machines available
        in the specified subscription. Results include VM names and are returned as a JSON array.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var computeService = context.GetService<IComputeService>();
            var vms = await computeService.GetVirtualMachines(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = vms?.Count > 0 ?
                new { vms } :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing virtual machines.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}