// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using Azure;
using AzureMcp.Areas.VirtualMachines.Commands;
using AzureMcp.Areas.VirtualMachines.Options.Vm;
using AzureMcp.Areas.VirtualMachines.Services;
using AzureMcp.Commands;
using AzureMcp.Models;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.VirtualMachines.Commands.Vm;

public sealed class VmListCommand(ILogger<VmListCommand> logger) 
    : BaseVirtualMachinesCommand<VmListOptions>
{
    private const string CommandTitle = "List Virtual Machines";
    private readonly ILogger<VmListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List virtual machines in a subscription or resource group. This command retrieves all virtual 
        machines available in the specified subscription or optionally within a specific resource group.
        Results include VM name, resource group, location, power status, size, and OS type.
        
        Optional parameters:
        - --resource-group: Filter to a specific resource group
        """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            AddSubscriptionInformation(context.Activity, options);

            var virtualMachinesService = context.GetService<IVirtualMachinesService>();
            var virtualMachines = await virtualMachinesService.ListVirtualMachinesAsync(
                options.Subscription!,
                options.ResourceGroup,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = virtualMachines?.Count > 0
                ? ResponseResult.Create(
                    new VmListCommandResult(virtualMachines),
                    VirtualMachinesJsonContext.Default.VmListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing virtual machines. Subscription: {Subscription}, ResourceGroup: {ResourceGroup}, Options: {@Options}",
                options.Subscription, options.ResourceGroup, options);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        RequestFailedException reqEx when reqEx.Status == 404 =>
            "Resource not found. Verify the subscription and resource group exist and you have access.",
        RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing virtual machines. Details: {reqEx.Message}",
        RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record VmListCommandResult(List<VirtualMachineInfo> VirtualMachines);
}