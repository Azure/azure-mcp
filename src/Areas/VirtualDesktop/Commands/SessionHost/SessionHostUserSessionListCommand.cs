// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using AzureMcp.Areas.VirtualDesktop.Models;
using AzureMcp.Areas.VirtualDesktop.Options.SessionHost;
using AzureMcp.Areas.VirtualDesktop.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;

public sealed class SessionHostUserSessionListCommand(ILogger<SessionHostUserSessionListCommand> logger) 
    : BaseSessionHostCommand<SessionHostUserSessionListOptions>
{
    private const string CommandTitle = "List User Sessions on Session Host";
    private readonly ILogger<SessionHostUserSessionListCommand> _logger = logger;

    public override string Name => "usersession-list";

    public override string Description =>
        """
        List all user sessions on a specific session host in a host pool. This command retrieves all Azure Virtual Desktop 
        user session objects available on the specified session host. Results include user session details such as 
        user principal name, session state, application type, and creation time.
          Required options:
        - subscription: Azure subscription ID or name
        - hostpool-name: Name of the host pool containing the session host (OR)
        - hostpool-resource-id: Resource ID of the host pool (alternative to hostpool-name)
        - sessionhost-name: Name of the session host to list user sessions from
          Optional options:
        - resource-group: Resource group name (when specified with hostpool-name, avoids subscription-wide search)
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
            IReadOnlyList<UserSession> userSessions;
            
            if (!string.IsNullOrEmpty(options.HostPoolResourceId))
            {
                userSessions = await virtualDesktopService.ListUserSessionsByResourceIdAsync(
                    options.Subscription!,
                    options.HostPoolResourceId,
                    options.SessionHostName!,
                    options.Tenant,
                    options.RetryPolicy);
            }
            else if (!string.IsNullOrEmpty(options.ResourceGroup))
            {
                userSessions = await virtualDesktopService.ListUserSessionsByResourceGroupAsync(
                    options.Subscription!,
                    options.ResourceGroup,
                    options.HostPoolName!,
                    options.SessionHostName!,
                    options.Tenant,
                    options.RetryPolicy);
            }
            else
            {
                userSessions = await virtualDesktopService.ListUserSessionsAsync(
                    options.Subscription!,
                    options.HostPoolName!,
                    options.SessionHostName!,
                    options.Tenant,
                    options.RetryPolicy);
            }

            context.Response.Results = userSessions.Count > 0
                ? ResponseResult.Create(new SessionHostUserSessionListCommandResult(userSessions.ToList()), VirtualDesktopJsonContext.Default.SessionHostUserSessionListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing user sessions for session host {SessionHostName} in hostpool {HostPoolName} / {HostPoolResourceId}", 
                options.SessionHostName, options.HostPoolName, options.HostPoolResourceId);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        RequestFailedException rfEx when rfEx.Status == 404 =>
            "Session host or hostpool not found. Verify the names and that you have access to them.",
        RequestFailedException rfEx when rfEx.Status == 403 =>
            "Access denied. Verify you have the necessary permissions to access the session host and hostpool.",
        RequestFailedException rfEx => rfEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        RequestFailedException rfEx => rfEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record SessionHostUserSessionListCommandResult(List<UserSession> UserSessions);
}
