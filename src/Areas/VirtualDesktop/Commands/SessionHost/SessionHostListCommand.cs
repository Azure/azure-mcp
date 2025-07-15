// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.ResourceManager.DesktopVirtualization;
using AzureMcp.Areas.VirtualDesktop.Commands.Hostpool;
using AzureMcp.Areas.VirtualDesktop.Models;
using AzureMcp.Areas.VirtualDesktop.Options;
using AzureMcp.Areas.VirtualDesktop.Options.SessionHost;
using AzureMcp.Areas.VirtualDesktop.Services;
using AzureMcp.Commands;
using AzureMcp.Models.Command;
using AzureMcp.Models.Option;
using Microsoft.Extensions.Logging;
using System.CommandLine.Parsing;

namespace AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;

public sealed class SessionHostListCommand(ILogger<SessionHostListCommand> logger) : BaseHostPoolCommand<SessionHostListOptions>
{
    private const string CommandTitle = "List SessionHosts";
    private readonly ILogger<SessionHostListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all SessionHosts in a hostpool. This command retrieves all Azure Virtual Desktop SessionHost objects available
        in the specified {OptionDefinitions.Common.Subscription} and {VirtualDesktopOptionDefinitions.HostPoolName}. Results include SessionHost details and are
        returned as a JSON array.
          Required options:
        - subscription: Azure subscription ID or name
        - hostpool-name: Name of the hostpool to list session hosts from
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

            var virtualDesktopService = context.GetService<IVirtualDesktopService>();
            var sessionHosts = await virtualDesktopService.ListSessionHostsAsync(
                options.Subscription!,
                options.HostPoolName!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = sessionHosts.Count > 0
                ? ResponseResult.Create(new SessionHostListCommandResult(sessionHosts.ToList()), VirtualDesktopJsonContext.Default.SessionHostListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing session hosts for hostpool {HostPoolName}", options.HostPoolName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        RequestFailedException rfEx when rfEx.Status == 404 =>
            "Hostpool not found. Verify the hostpool name and that you have access to it.",
        RequestFailedException rfEx when rfEx.Status == 403 =>
            "Access denied. Verify you have the necessary permissions to access the hostpool.",
        RequestFailedException rfEx => rfEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        RequestFailedException rfEx => rfEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record SessionHostListCommandResult(List<Models.SessionHost> SessionHosts);
}


