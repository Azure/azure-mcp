// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using Azure.Identity;
using AzureMcp.Areas.VirtualDesktop.Options.Hostpool;
using AzureMcp.Areas.VirtualDesktop.Services;
using AzureMcp.Models.Option;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.VirtualDesktop.Commands.Hostpool;

public sealed class HostpoolListCommand(ILogger<HostpoolListCommand> logger) : BaseVirtualDesktopCommand<HostpoolListOptions>()
{
    private const string CommandTitle = "List hostpools";
    private readonly ILogger<HostpoolListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all hostpools in a subscription. This command retrieves all Azure Virtual Desktop hostpool objects available
        in the specified {OptionDefinitions.Common.Subscription}. Results include hostpool names and are
        returned as a JSON array.
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

            var hostpools = await virtualDesktopService.ListHostpoolsAsync(
                options.Subscription!, 
                options.Tenant, 
                options.RetryPolicy);
                
            context.Response.Results = hostpools.Count > 0
                ? ResponseResult.Create(new HostPoolListCommandResult(hostpools.ToList()), VirtualDesktopJsonContext.Default.HostPoolListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing hostpools. Subscription: {Subscription}, Options: {@Options}",
                options.Subscription, options);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        AuthenticationFailedException => "Authentication failed. Please run 'az login' to sign in or check your credentials.",
        RequestFailedException rfEx when rfEx.Status == 403 => "Access denied. Verify you have Virtual Desktop permissions for this subscription.",
        RequestFailedException rfEx when rfEx.Status == 404 => "Subscription not found or no hostpools exist in this subscription.",
        RequestFailedException rfEx => rfEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        AuthenticationFailedException => 401,
        RequestFailedException rfEx => rfEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record HostPoolListCommandResult(List<Models.HostPool> hostpools);
}

