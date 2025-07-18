// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using AzureMcp.Areas.VirtualDesktop.Models;
using AzureMcp.Areas.VirtualDesktop.Options.SessionHost;
using AzureMcp.Areas.VirtualDesktop.Services;
using AzureMcp.Commands;
using AzureMcp.Models.Command;
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
        - hostpool-name: Name of the host pool containing the session host
        - sessionhost-name: Name of the session host to list user sessions from
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
            var userSessions = await virtualDesktopService.ListUserSessionsAsync(
                options.Subscription!,
                options.HostPoolName!,
                options.SessionHostName!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = userSessions.Count > 0
                ? ResponseResult.Create(new SessionHostUserSessionListCommandResult(userSessions.ToList()), VirtualDesktopJsonContext.Default.SessionHostUserSessionListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing user sessions for session host {SessionHostName} in hostpool {HostPoolName}", 
                options.SessionHostName, options.HostPoolName);
            HandleException(context.Response, ex);
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
