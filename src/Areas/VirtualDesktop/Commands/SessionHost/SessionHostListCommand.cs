// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using AzureMcp.Areas.VirtualDesktop.Commands.Hostpool;
using AzureMcp.Areas.VirtualDesktop.Options;
using AzureMcp.Areas.VirtualDesktop.Options.SessionHost;
using AzureMcp.Areas.VirtualDesktop.Services;
using AzureMcp.Models.Option;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;

public sealed class SessionHostListCommand(ILogger<SessionHostListCommand> logger) : BaseHostPoolCommand<SessionHostListOptions>
{
    private const string CommandTitle = "List SessionHosts";
    private readonly ILogger<SessionHostListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all SessionHosts in a hostpool. This command retrieves all Azure Virtual Desktop SessionHost objects available
        in the specified {OptionDefinitions.Common.Subscription} and hostpool. Results include SessionHost details and are
        returned as a JSON array.
          Required options:
        - subscription: Azure subscription ID or name
        - hostpool-name: Name of the hostpool to list session hosts from (OR)
        - hostpool-resource-id: Resource ID of the hostpool (alternative to hostpool-name)
          Note: Either hostpool-name or hostpool-resource-id must be provided, but not both.
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

            // Validate that either hostpool-name or hostpool-resource-id is provided, but not both
            if (string.IsNullOrEmpty(options.HostPoolName) && string.IsNullOrEmpty(options.HostPoolResourceId))
            {
                context.Response.Status = 400;
                context.Response.Message = "Either --hostpool-name or --hostpool-resource-id must be provided.";
                return context.Response;
            }

            if (!string.IsNullOrEmpty(options.HostPoolName) && !string.IsNullOrEmpty(options.HostPoolResourceId))
            {
                context.Response.Status = 400;
                context.Response.Message = "Cannot specify both --hostpool-name and --hostpool-resource-id. Use only one.";
                return context.Response;
            }

            var virtualDesktopService = context.GetService<IVirtualDesktopService>();
            IReadOnlyList<Models.SessionHost> sessionHosts;
            
            if (!string.IsNullOrEmpty(options.HostPoolResourceId))
            {
                sessionHosts = await virtualDesktopService.ListSessionHostsByResourceIdAsync(
                    options.Subscription!,
                    options.HostPoolResourceId,
                    options.Tenant,
                    options.RetryPolicy);
            }
            else
            {
                sessionHosts = await virtualDesktopService.ListSessionHostsAsync(
                    options.Subscription!,
                    options.HostPoolName!,
                    options.Tenant,
                    options.RetryPolicy);
            }

            context.Response.Results = sessionHosts.Count > 0
                ? ResponseResult.Create(new SessionHostListCommandResult(sessionHosts.ToList()), VirtualDesktopJsonContext.Default.SessionHostListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing session hosts for hostpool {HostPoolName} / {HostPoolResourceId}", 
                options.HostPoolName, options.HostPoolResourceId);
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


